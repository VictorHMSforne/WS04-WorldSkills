using System;
using System.Collections.Generic;
using System.Text;
using WS04.Models;
using SQLite;


namespace WS04.Services
{
    public class ServicesDB
    {
        SQLiteConnection con;
        public string StatusMessage { get; set; } //chama no back do front-end

        public ServicesDB(string dbPath)
        {
            if (dbPath == "") dbPath = App.DbPath;
            con = new SQLiteConnection(dbPath);
            con.CreateTable<Paciente>();
            
        }

        public void Inserir(Paciente paciente)
        {
            
            try
            {
                if (string.IsNullOrEmpty(paciente.Nome_Paciente))
                    throw new Exception("Nome do Paciente não informado");
                if (string.IsNullOrEmpty(paciente.Nome_Doutor))
                    throw new Exception("Nome do Médico não informado");
                string sql = "SELECT Nome_Paciente FROM Paciente WHERE Nome_Paciente='" + paciente.Nome_Paciente + "'";
                //var search = con.ExecuteScalar<Paciente>(sql,paciente.Nome_Paciente);
                //con.Find<Paciente>(paciente);
                var search = con.FindWithQuery<Paciente>(sql, paciente.Nome_Paciente);
                if (search != null)
                {
                    this.StatusMessage = string.Format("Paciente já cadastrado!");
                    return;
                }
                else
                {
                    int result = con.Insert(paciente);
                    if (result != 0)
                    {
                        this.StatusMessage = string.Format("Cadastro realizado com Sucesso!");
                    }
                    else
                    {
                        this.StatusMessage = string.Format("Não foi Possível realizar o Cadastro!");
                    }
                }

                
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public List<Paciente> Listar()
        {
            List<Paciente> lista = new List<Paciente>();
            try
            {
                lista = con.Table<Paciente>().ToList(); //Mesma coisa do SELECT * FROM
                
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            return lista;
        }

        public void Editar(Paciente paciente)
        {
            try
            {
                if (paciente.Id <= 0)
                    throw new Exception("Por favor digite um Id Válido para realizar a edição do cadastro.");
                int result = con.Update(paciente);
                StatusMessage = string.Format("Registro alterado com Sucesso");
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public void Excluir(int Id)
        {
            try
            {
                int result = con.Table<Paciente>().Delete(r => r.Id == Id);
                StatusMessage = string.Format("Registro Deletado com Sucesso!");
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public List<Paciente> Localizar(string nome)
        {
            List<Paciente> lista = new List<Paciente>();
            try
            {
                var resp = from p in con.Table<Paciente>() where p.Nome_Paciente.ToLower().Contains(nome.ToLower()) select p;
                lista = resp.ToList();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            return lista;
        }

        public Paciente GetPaciente(int Id)
        {
            Paciente p = new Paciente();
            try
            {
                p = con.Table<Paciente>().First(n => n.Id == Id);
                StatusMessage = string.Format("Encontrei o Paciente");

            }
            catch (Exception erro)
            {

                throw new Exception(erro.Message);
            }
            return p;
        }
    }
}
