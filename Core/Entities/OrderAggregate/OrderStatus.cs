using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        //this is so that we can receive text from here, othervise
        //it will return 0, 1, 2
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Received")]
        PaymentRecevied,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}