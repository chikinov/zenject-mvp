namespace Zenject.MVP
{
    public class AlertDialogFactory :
        DialogFactory<string, string, string, string,
            AlertDialogPool, AlertDialog>
    {
        public override AlertDialog Create(
            string title,
            string message,
            string positiveText,
            string negativeText) =>
            base.Create(title, message, positiveText, negativeText);
    }
}
