namespace XM.UI.Event
{
    internal interface IGuiAcceptsPriceChange
    {
        void ChangePrice(string recordId, int price);
    }
}
