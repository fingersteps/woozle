using ServiceStack.ServiceHost;

namespace Woozle.Services.Mandator
{
    [Route("/mandatorselect", "POST")]
    public class MandatorSelect : IReturn<bool>
    {
        public Mandator SelectedMandator { get; set; }
    }
}
