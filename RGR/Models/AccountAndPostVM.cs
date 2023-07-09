namespace RGR.Models
{
    public class AccountAndPostVM
    {
        private readonly DbForRgrContext db = new();
        public Post post { get; set; }
        public Account account { get; set; }
        public List<Post> posts { get; set; }
        public AccountAndPostVM(int idPost,int idUser)
        {
            post = db.Posts.Where(p => p.IdPost == idPost).FirstOrDefault();
            account=db.Accounts.Where(a=>a.IdAccount == idUser).FirstOrDefault();
            posts=db.Posts.Where(p=>p.IdAccount==idUser).ToList();
        }
    }
}
