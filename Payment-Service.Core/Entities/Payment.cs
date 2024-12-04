namespace Payment_Service.Core.Entities;

public class Payment
{
    public int PaymentId { get; set; }      
    public double Amount { get; set; }
    public int OrderId { get; set; }
    public DateTime PaymentDate { get; set; }   
    public string PaymentMethod { get; set; }   
    public bool IsSuccessful { get; set; }
}