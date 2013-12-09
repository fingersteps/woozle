using ServiceStack.ServiceHost;

namespace Woozle.Services.Mandator
{
    [Route("/mandatorselect", "POST")]
    public class MandatorSelectDto : IReturn<bool>
    {
        public MandatorDto SelectedMandatorDto { get; set; }
    }
}
