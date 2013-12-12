using Moq;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Testing;
using Woozle.Model.SessionHandling;

namespace Woozle.UnitTest.Services
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

        public IRequestContext GetRequestContextMock(Session session)
        {
            var mockedRequestContext = new MockRequestContext();
            mockedRequestContext.Get<IHttpRequest>().Items.Add(
            ServiceExtensions.RequestItemsSessionKey, session);
            return mockedRequestContext;
        }
    }
}
