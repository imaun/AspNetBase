namespace AspNetBase.Domain
{
    public interface IHasMetaData<TUserId>
    {
        DateTime CreateDate { get; set; }
        DateTime? ModifyDate { get; set; }
        TUserId? CreatedByUserId { get; set; }
        TUserId? ModifiedByUserId { get; set; }
        string CreatedByIp { get; set; }
        string ModifiedByIp { get; set; }
    }

    public abstract class HasMetaData<TUserId> : IHasMetaData<TUserId>
    {
        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
        public TUserId? CreatedByUserId { get; set; }
        public TUserId? ModifiedByUserId { get; set; }
        public string CreatedByIp { get; set; }
        public string ModifiedByIp { get; set; }
    }
}
