namespace XM.Progression.Craft
{
    public class RecipeItem
    {
        public string Resref { get; set; }
        public int Quantity { get; set; }

        public RecipeItem(string resref, int quantity)
        {
            Resref = resref;
            Quantity = quantity;
        }
    }
}
