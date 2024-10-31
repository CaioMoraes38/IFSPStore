using IFSPStore.Domain.Entities;
using System.Text.Json;

namespace IFSPStore.Teste
{
    [TestClass]
    public class UnitDomain
    {
        [TestMethod]
        public void TestCidade()
        {
            Cidade cidade = new Cidade(1, "Birigui", "SP");
            Console.WriteLine(JsonSerializer.Serialize(cidade));
            Assert.AreEqual(cidade.Nome, "Birigui");
            Assert.AreEqual(cidade.Estado, "SP");
        }

    }
}