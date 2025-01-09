using EnergencyService.Models.AutoReg;
using EnergencyService.Models.Template;
using EnergencyService.DataBaseClasses;
using EnergencyService.RabbitMQ;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using EmergencyService.Classes;

namespace EmergencyService.Classes
{
    public class Registration
    {
        FullAccountTemplate fullModelAccount;
        public Registration(FullAccountTemplate fullModelAccount) {
            if (fullModelAccount is null)
                Console.WriteLine("[Process is failed] FullAccountTemplate is null!");
            else
                this.fullModelAccount = fullModelAccount;
        }

        public bool Register()
        {
            bool isChekUser = false;
            Guid info = Guid.NewGuid();
            Guid account = Guid.NewGuid();
            using (var context = new ApplicationContext())
            {
                try
                {
                    Console.WriteLine("[Process...] Create Info User");
                    RegistrationModel? registrationModel = context.Account_Info.FirstOrDefault(a=>a.username==fullModelAccount.UserName);
                    if (registrationModel is not null)
                    {
                        Console.WriteLine("[Stop!] Select one Info User");
                        isChekUser = true;
                    }
                    else
                    {
                        RegistrationModel? RegModel = new RegistrationModel
                        {
                            username = fullModelAccount.UserName,
                            date = (DateTime)fullModelAccount.Dateofbirths
                        };
                        info = RegModel.id;

                        context.Account_Info.AddRange(RegModel);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex}");
                }
                
            }
            using (var context = new ApplicationContext())
            {
                try
                {
                    Console.WriteLine("[Process...] Create Account User");
                    ModuleHash moduleHash = new ModuleHash();
                    string HashPass = moduleHash.GetHash(fullModelAccount.password);
                    AutorizationModel? autoreg =  context.Account.FirstOrDefault(a=>a.email==fullModelAccount.Email);
                    Console.WriteLine("[Process...Hash?] "+HashPass);
                    if (autoreg is not null)
                    {
                        Console.WriteLine("[Stop!] Select one Account User");
                        isChekUser = true;
                    }
                    else
                    {
                        AutorizationModel? AutoModel = new AutorizationModel
                        {
                            email = fullModelAccount.Email,
                            password = HashPass
                        };
                        context.Account.AddRange(AutoModel);
                        account = AutoModel.id;
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex}");
                }

            }
            using (var context = new ApplicationContext())
            {
                try
                {

                    Console.WriteLine("[Process...] Create User");
                    UserModel? user = context.User.FirstOrDefault(a=>a.id_info==info && a.id_account==account);
                    if (user is not null)
                    {
                        Console.WriteLine("[Stop!] Select one Account User");
                        isChekUser = true;
                    }
                    else
                    {
                        UserModel? users = new UserModel()
                        {
                            id_info = info,
                            id_account = account,
                            id_avatar = null,
                        };
                        RabbitMq rabbitMQ = new RabbitMq();
                        rabbitMQ.SendMessage(users.id, "KeyUser");
                        context.User.AddRange(users);
                        context.SaveChanges();
                    } 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex}");
                }
            }
            return isChekUser; 
        }
    }
}
