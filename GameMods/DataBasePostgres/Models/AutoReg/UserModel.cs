namespace DataBasePostgres.Models.AutoReg
{
    public class UserModel
    {
        public Guid id { get; set; }
        public Guid id_account { get; set; }
        public Guid id_info { get; set; }
        public Guid id_icon { get; set; }
        public UserModel()
        {
            id = Guid.NewGuid();
        }
    }
}
