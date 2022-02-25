namespace AspNetBase.Domain.Models {

    public class AppClaimType : IBaseEntity {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
