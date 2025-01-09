namespace EnergencyService.Models.AutoReg
{
    public class UserModel
    {
        public Guid id { get; set; }
        public Guid id_account { get; set; }
        public Guid id_info { get; set; }
        public Guid? id_avatar { get; set; }
        public UserModel()
        {
            id = Guid.NewGuid();
        }
    }
}
