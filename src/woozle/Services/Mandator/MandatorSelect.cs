using ServiceStack;

namespace Woozle.Services.Location
{
    [Route("/mandatorselect", "POST")]
    public class MandatorSelect : IReturn<bool>
    {
        public Mandator.Mandator SelectedMandator { get; set; }
    }
}
