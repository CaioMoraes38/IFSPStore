using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFSPStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


namespace ProjetoTeste
{
    [TestClass]
    public class UnitTestReposotory
    {
        public partial class MyDbContext : DbContext
        {
            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Cidade> Cidades { get; set; }
            public DbSet<Cliente> Clientes { get; set; }
            public DbSet<Grupo> Grupos { get; set; }
            public DbSet<Produto> Produtos { get; set; }
            public DbSet<Venda> Vendas { get; set; }
            public DbSet<VendaItem> VendaItenns { get; set; }

            public MyDbContext()
            {
                Database.EnsureCreated();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                var server = "localhost";
                var port = "3306";
                var database = "IFSPStore";
                var username = "root";
                var password = "";
                var strCon = $"Server={server};Port={port};database={database};Uid={username};Pwd={password}";

                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseMySql(strCon, ServerVersion.AutoDetect(strCon));
                }
            }
        }

        [TestMethod]
        public void TestInsertCidade()
        {
            using (var context = new MyDbContext())
            {
                var cidade = new Cidade
                {
                    Nome = "Birigui",
                    Estado = "SP"
                };
                context.Cidades.Add(cidade);

                cidade = new Cidade
                {
                    Nome = "Araçatuba",
                    Estado = "SP"
                };
                context.Cidades.Add(cidade);

                context.SaveChanges();
            }
        }

        [TestMethod]

        public void TestListCidade()
        {
            using (var context = new MyDbContext())
            {
                var cidades = context.Cidades.ToList();
                foreach (var cidade in cidades)
                {
                    Console.WriteLine(cidade.Nome);
                }
            }
        }

        [TestMethod]
        public void TestInsertCliente()
        {
            using (var context = new MyDbContext())
            {
                var cidade = context.Cidades.FirstOrDefault();
                var cliente = new Cliente
                {
                    Nome = "João",
                    Endereco = "Rua 1",
                    Bairro = "Centro",
                    Cidade = cidade
                };
                context.Clientes.Add(cliente);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void TestListCliente()
        {
            using (var context = new MyDbContext())
            {
                var clientes = context.Clientes.Include(x => x.Cidade).ToList();
                foreach (var cliente in clientes)
                {
                    Console.WriteLine(cliente.Nome);
                    Console.WriteLine(cliente.Cidade.Nome);
                }
            }
        }

        /*[TestMethod]
        public void TestDeleteCliente()
        {
            using (var context = new MyDbContext())
            {
                var cliente = context.Clientes.FirstOrDefault();
                context.Clientes.Remove(cliente);
                context.SaveChanges();
            }
        }*/
        [TestMethod]
        public void TestInsertGrupo()
        {
            using (var context = new MyDbContext())
            {
                var grupo = new Grupo
                {
                    Nome = "Eletrônicos"
                };
                context.Grupos.Add(grupo);

                grupo = new Grupo
                {
                    Nome = "Móveis"
                };
                context.Grupos.Add(grupo);

                context.SaveChanges();
            }
        }
        [TestMethod]
        public void TestlistGrupo()
        {
            using (var context = new MyDbContext())
            {
                var grupos = context.Grupos.ToList();
                foreach (var grupo in grupos)
                {
                    Console.WriteLine(grupo.Nome);
                }
            }
        }

        [TestMethod]
        public void TestInsertProduto()
        {
            using (var context = new MyDbContext())
            {
                var grupo = context.Grupos.FirstOrDefault();
                var produto = new Produto
                {
                    Nome = "TV",
                    Preco = 9000,
                    Quantidade = 10,
                    DataCompra = DateTime.Now,
                    UnidadeVenda = "seila",
                    Grupo = grupo
                };
                context.Produtos.Add(produto);
                context.SaveChanges();
            }
        }

        [TestMethod]

        public void TestListProduto()
        {
            using (var context = new MyDbContext())
            {
                var produtos = context.Produtos.Include(x => x.Grupo).ToList();
                foreach (var produto in produtos)
                {
                    Console.WriteLine(produto.Nome);
                    Console.WriteLine(produto.Grupo.Nome);
                }
            }
        }

       /*[TestMethod]
        public void TestDeleteProduto()
        {
            using (var context = new MyDbContext())
            {
                var produto = context.Produtos.FirstOrDefault();
                context.Produtos.Remove(produto);
                context.SaveChanges();
            }
        }*/

        [TestMethod]

        public void TestInsertUsuario()
        {
            using (var context = new MyDbContext())
            { 
                var usario = new Usuario
                {
                    Nome = "Caio",
                    Senha = "123",
                    Login = "caio",
                    Email = "caio.moraes.santos@gmail.com"
                };
                context.Usuarios.Add(usario);
                context.SaveChanges();

            }
        }
       
        [TestMethod]
        public void TestListUsuario()
        {
            using (var context = new MyDbContext())
            {
                var usuarios = context.Usuarios.ToList();
                foreach (var usuario in usuarios)
                {
                    Console.WriteLine(usuario.Nome);
                }
            }
        }
        
        [TestMethod]
        public void TestInsertVendas()
        {
            using (var context = new MyDbContext())
            {
                var cliente = context.Clientes.FirstOrDefault();
                var usuario = context.Usuarios.FirstOrDefault();
                var produto = context.Produtos.FirstOrDefault();
                var venda = new Venda
                {
                    Data = DateTime.Now,
                    ValorTotal = 2000,
                    Cliente = cliente,
                    Usuario = usuario
                };
                context.Vendas.Add(venda);
                context.SaveChanges();

                var vendaItem = new VendaItem
                {
                    Produto = produto,
                    Quantidade = 1,
                    ValorUnitario = 2000,
                    ValorTotal = 2000,
                    Venda = venda
                };
                context.VendaItenns.Add(vendaItem);
                context.SaveChanges();
            }
        }

        [TestMethod]

        public void TestListVendas()
        {
            using (var context = new MyDbContext())
            {
                var vendas = context.Vendas.Include(x => x.Cliente).Include(x => x.Usuario).ToList();
                foreach (var venda in vendas)
                {
                    Console.WriteLine(venda.Cliente.Nome);
                    Console.WriteLine(venda.Usuario.Nome);
                }
            }
        }


      
    }

}