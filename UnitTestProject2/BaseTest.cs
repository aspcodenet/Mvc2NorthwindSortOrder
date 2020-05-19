using System.Linq;
using AutoFixture;

namespace UnitTestProject2
{
    public class BaseTest
    {
        protected Fixture fixture = new Fixture();

        public BaseTest()
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}