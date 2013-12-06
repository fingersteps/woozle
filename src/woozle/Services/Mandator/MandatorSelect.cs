using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.Mandator
{
    [Route("/mandatorselect", "POST")]
    public class MandatorSelect : IReturn<bool>
    {
        public Mandator SelectedMandator { get; set; }
    }
}
