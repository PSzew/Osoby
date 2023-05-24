using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace Miras
{
    public class Person
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }
        public Person(string imie,string nazwisko,int wiek)
        {
            Imie = imie;
            Nazwisko= nazwisko;
            Wiek = wiek;
            
        }
        public Person()
        {

        }
    }
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database= new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>().Wait();
        }
        public Task<List<Person>> GetPersonAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }
        public Task<int> SavePeopleAsync(Person person)
        {
            return _database.InsertAsync(person);
        }
        public Task<int> DropDbAsync(Person p)
        {
            return _database.DeleteAsync(p);
        }
        public Task<int> EditPeopleAsync(Person p)
        {
            return _database.UpdateAsync(p);
        }

    }
}
