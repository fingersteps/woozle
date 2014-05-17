using Moq;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Testing;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Test.Services
{
    public abstract class SessionTestBase
    {
        public Mock<IRequestContext> GetRequestContextMock()
        {
            var requestContextMock = new Mock<IRequestContext>();
            requestContextMock.Setup(n => n.Get<IHttpRequest>())
                              .Returns(new MockHttpRequest());
            return requestContextMock;
        }

        public IRequestContext GetFakeRequestContext()
        {
            return GetFakeRequestContext(new Session(new SessionData(new User(), new Model.Mandator())));
        }

        public IRequestContext GetFakeRequestContext(Session session)
        {
            var mockedRequestContext = new MockRequestContext();
            mockedRequestContext.Get<IHttpRequest>().Items.Add(
                ServiceExtensions.RequestItemsSessionKey, session);
            return mockedRequestContext;
        }
    }
}
