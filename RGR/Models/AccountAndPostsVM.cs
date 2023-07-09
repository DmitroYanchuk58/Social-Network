namespace RGR.Models
{
    public class AccountAndPostsVM
    {
        private readonly DbForRgrContext db = new();
        public Account account = new();
        public List<Post> posts = new();
        public AccountAndPostsVM(int id)
        {
            account=db.Accounts.Where(a=>a.IdAccount==id).FirstOrDefault();
            posts=db.Posts.Where(p=>p.IdAccount==id).ToList();
        }
    }
}
