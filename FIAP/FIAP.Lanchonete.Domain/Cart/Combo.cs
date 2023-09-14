namespace FIAP.Diner.Domain.Cart;

public class Combo
{
    Product? MainDish {  get; set; }
    Product? Dessert { get; set; }
    Product? SideDish { get; set; }
    Product? Drink {  get; set; }   
}
