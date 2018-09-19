using System.Data.Entity;
using Who.Data;
using Who.Data.Enums;

namespace Who.DAL.DatabaseInitialize
{
    public class ApplicationDbContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.MetaDataEntities.Add(new MetaDataEntity
            {
                Name = MetaDataEnum.ImagesPerRound.ToString(),
                Value = 4.ToString(),
                Type = MetaDataTypeEnum.Int.ToString()
            });

            context.MetaDataEntities.Add(new MetaDataEntity
            {
                Name = MetaDataEnum.RoundsPerGame.ToString(),
                Value = 5.ToString(),
                Type = MetaDataTypeEnum.Int.ToString()
            });

            context.Users.Add(new UserEntity
            {
                FullName = "Demo Tester"
            });
            context.Images.Add(new ImageEntity { Name = "Henry Cavill", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/cf5822bd-80e2-44be-8720-5b15ce963bbf.jpg" });
            context.Images.Add(new ImageEntity { Name = "Alexandra Daddario", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/4976a068-d8f9-44ed-98a6-cc880f6be058.jpg" });
            context.Images.Add(new ImageEntity { Name = "Jason Statham", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/0b1ef5b0-db64-4fd6-9c7f-52d1a28c5b70.jpg" });
            context.Images.Add(new ImageEntity { Name = "Scarlett Johansson", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/6f24c66e-dc8c-418a-a16d-cb502bcab8c1.jpg" });
            context.Images.Add(new ImageEntity { Name = "Rose Byrne", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/b704cd3c-ae35-4284-af99-f3f9306113b2.jpg" });
            context.Images.Add(new ImageEntity { Name = "R. Sarathkumar", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/c52d3f00-74c3-4b08-a7e3-50013b3b64e2.jpg" });
            context.Images.Add(new ImageEntity { Name = "Brad Pitt", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/137f104d-b512-4b70-846d-db26d20cd8d0.jpg" });
            context.Images.Add(new ImageEntity { Name = "Kate Beckinsale", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/8779a2fc-38db-4416-923e-e32d2cdd5a9f.jpg" });
            context.Images.Add(new ImageEntity { Name = "Samuel L. Jackson", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/a0841691-c393-411e-9764-bcd4ed89e586.jpg" });
            context.Images.Add(new ImageEntity { Name = "Milly Shapiro", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/6c7271b1-0696-49e7-a138-947eab5d6485.jpg" });
            context.Images.Add(new ImageEntity { Name = "Tom Cruise", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/b172e8d4-2859-4675-987e-ab52ef83708e.jpg" });
            context.Images.Add(new ImageEntity { Name = "Beyonc� Knowles", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/868a6f52-f862-4970-8df1-c1d9f9acdc98.jpg" });
            context.Images.Add(new ImageEntity { Name = "Matilda Anna Ingrid Lutz", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/8fe49a0b-7cca-4f1b-8fc3-cc3af36f8bc9.jpg" });
            context.Images.Add(new ImageEntity { Name = "Cate Blanchett", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/28108c4c-58a3-41bb-9a3c-d02f75abe807.jpg" });
            context.Images.Add(new ImageEntity { Name = "Tom Hardy", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/aff1a9c8-4c3c-4932-959d-e8564fd1f288.jpg" });
            context.Images.Add(new ImageEntity { Name = "Robert Downey Jr.", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/d87d9bf3-173b-4543-93ad-556af20ab776.jpg" });
            context.Images.Add(new ImageEntity { Name = "Sean Bean", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/69aa53fb-b3df-43fc-83be-c8331d6f2150.jpg" });
            context.Images.Add(new ImageEntity { Name = "Carla Gugino", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/cca63048-62b2-4396-81ef-cb5fda5d9a5c.jpg" });
            context.Images.Add(new ImageEntity { Name = "Chris Hemsworth", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/0078c971-de82-4386-9beb-61ef6ab937bc.jpg" });
            context.Images.Add(new ImageEntity { Name = "Josh Brolin", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/0a6dcdc5-4b2e-4216-8b74-8e9897f2efd9.jpg" });
            context.Images.Add(new ImageEntity { Name = "Liam Neeson", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/7ddbf45c-7a16-4469-951a-76ae724c22b9.jpg" });
            context.Images.Add(new ImageEntity { Name = "Nicolas Cage", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/f860ff71-1782-4fac-9f99-319ee3233372.jpg" });
            context.Images.Add(new ImageEntity { Name = "Jordan Burtchett", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/c6e40e53-5b8f-45c7-996e-2db33b48c987.jpg" });
            context.Images.Add(new ImageEntity { Name = "Johnny Depp", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/19531138-1772-451f-bcda-fb9642fbcab7.jpg" });
            context.Images.Add(new ImageEntity { Name = "Michael Provost", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/6e37b3ae-2d46-4f1e-bb06-8189bafa69ad.jpg" });
            context.Images.Add(new ImageEntity { Name = "Vin Diesel", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/187cc5cf-bab5-440a-bb5f-5632577a76ee.jpg" });
            context.Images.Add(new ImageEntity { Name = "Tom Hanks", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/bed29ce7-c72a-42e1-be80-f9393ef2c134.jpg" });
            context.Images.Add(new ImageEntity { Name = "Morgan Freeman", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/9828da42-88ef-43d4-81a9-d4c4b14496cf.jpg" });
            context.Images.Add(new ImageEntity { Name = "�lvaro Morte", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/39450d5f-2f23-4718-bfa7-00fb9277727f.jpg" });
            context.Images.Add(new ImageEntity { Name = "Arnold Schwarzenegger", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/bcefef62-f2dc-4038-ba75-190cb590303d.jpg" });
            context.Images.Add(new ImageEntity { Name = "Charlize Theron", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/b812f8a3-1d18-489e-9684-a8cd789c21c9.jpg" });
            context.Images.Add(new ImageEntity { Name = "Noah Centineo", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/b28e4cd2-997e-445c-bd06-a7bb0275a612.jpg" });
            context.Images.Add(new ImageEntity { Name = "Dwayne Johnson", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/338300fb-39fa-4220-aa4e-5a5d8c36745d.jpg" });
            context.Images.Add(new ImageEntity { Name = "Melissa Sue Anderson", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/c3fa0370-0a4d-4d3e-ac0f-47676a8b4cd4.jpg" });
            context.Images.Add(new ImageEntity { Name = "Helena Bonham Carter", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/01f422da-791d-4281-975c-23ea50dc5ec3.jpg" });
            context.Images.Add(new ImageEntity { Name = "Christopher Nolan", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/f878c2af-793f-417f-9336-6bd16d790cbd.jpg" });
            context.Images.Add(new ImageEntity { Name = "Bryce Dallas Howard", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/3e8f9d4f-e951-4927-97db-76e406f5532e.jpg" });
            context.Images.Add(new ImageEntity { Name = "Itziar Itu�o", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/814ba668-a473-443c-9b90-d924f3412c52.jpg" });
            context.Images.Add(new ImageEntity { Name = "Harrison Ford", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/838e2d69-e0fb-44c6-b085-041e8ccd715d.jpg" });
            context.Images.Add(new ImageEntity { Name = "Kim Yoo-yeon", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/164e8180-a4bc-4dec-b082-fe1ea226d92d.jpg" });
            context.Images.Add(new ImageEntity { Name = "Chlo� Grace Moretz", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/86fd3136-2bb6-4671-ad24-a10a7ef9899c.jpg" });
            context.Images.Add(new ImageEntity { Name = "Lucy Liu", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/20653370-3b98-4717-afe0-aa9ec8b9b582.jpg" });
            context.Images.Add(new ImageEntity { Name = "Hugh Jackman", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/d18928d6-3198-4614-8fc4-643b35f98f94.jpg" });
            context.Images.Add(new ImageEntity { Name = "Will Smith", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/7fc9d028-4d49-41ba-ac2b-299f5bc75304.jpg" });
            context.Images.Add(new ImageEntity { Name = "Rachel McAdams", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/8f03950d-3e90-4e5f-aef0-560185026262.jpg" });
            context.Images.Add(new ImageEntity { Name = "Pan Chunchun", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/88ff9686-3c8d-40b6-9d86-a730a041f97a.jpg" });
            context.Images.Add(new ImageEntity { Name = "Monica Bellucci", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/b8c37adf-04ef-46b8-a480-00f51cb54d73.jpg" });
            context.Images.Add(new ImageEntity { Name = "Diane Lane", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/90e26441-ba7d-4cc8-b9bb-4e45a030bf7f.jpg" });
            context.Images.Add(new ImageEntity { Name = "Chris Evans", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/d8b1c0cb-4068-4bce-9585-629f64056a18.jpg" });
            context.Images.Add(new ImageEntity { Name = "Prabhas", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/69e24fd1-19c1-45a4-9dc9-893a958acb3a.jpg" });
            context.Images.Add(new ImageEntity { Name = "Sandra Bullock", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/fed3ce3b-a7c0-4d0b-98d4-891ae2b7d0c5.jpg" });
            context.Images.Add(new ImageEntity { Name = "Zoe Saldana", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/d4073a3c-a404-40d0-a599-f9871cf8c5fd.jpg" });
            context.Images.Add(new ImageEntity { Name = "Denzel Washington", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/564979fc-e0c1-4620-acd1-b6c0de831aed.jpg" });
            context.Images.Add(new ImageEntity { Name = "Emilia Clarke", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/20245fa2-6752-44cd-930a-788dc41f1063.jpg" });
            context.Images.Add(new ImageEntity { Name = "Kevin Bacon", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/9bbc9f9f-5156-4ac0-8a42-8211815de5be.jpg" });
            context.Images.Add(new ImageEntity { Name = "Rihanna", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/01c3dccc-1bd9-4e0d-acf8-3ee8cea21736.jpg" });
            context.Images.Add(new ImageEntity { Name = "Donnie Yen", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/31fdb698-da6a-4b8e-94a1-3bc426bc0ed9.jpg" });
            context.Images.Add(new ImageEntity { Name = "Anne Hathaway", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/7aebc70e-603c-4990-bba0-ff2341a1212c.jpg" });
            context.Images.Add(new ImageEntity { Name = "Sylvester Stallone", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/3f111396-7fcd-4ebc-95e7-d986788cc496.jpg" });
            context.Images.Add(new ImageEntity { Name = "Leonardo DiCaprio", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/4df771ac-c672-4d51-bbc7-9da85206fa2b.jpg" });
            context.Images.Add(new ImageEntity { Name = "Sean Connery", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/7b0e302e-58d1-4a5b-a125-1930a193a439.jpg" });
            context.Images.Add(new ImageEntity { Name = "Al Pacino", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/5ef920ca-1f4d-46e8-9add-41ccc9b3a77f.jpg" });
            context.Images.Add(new ImageEntity { Name = "Jennifer Lawrence", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/6d14571b-908f-48ff-83a6-306da11cad01.jpg" });
            context.Images.Add(new ImageEntity { Name = "Bill Nighy", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/4b8f37c6-d2bd-4216-96f9-6990386a2f6b.jpg" });
            context.Images.Add(new ImageEntity { Name = "Diane Kruger", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/7d041b86-d7b8-487a-a209-d6be34de0c67.jpg" });
            context.Images.Add(new ImageEntity { Name = "Kristen Stewart", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/e6b533f4-a415-49fd-b5f7-8578d8600f69.jpg" });
            context.Images.Add(new ImageEntity { Name = "John Travolta", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/33c926c4-419f-4b58-a6f0-0ce2f0dcfdae.jpg" });
            context.Images.Add(new ImageEntity { Name = "Ben Affleck", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/4261d6f3-f699-4a4e-aaeb-7e91d43ac5da.jpg" });
            context.Images.Add(new ImageEntity { Name = "Adam Sandler", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/dc1f1f2a-ae15-4098-9f39-5d13bbd95b03.jpg" });
            context.Images.Add(new ImageEntity { Name = "Keanu Reeves", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/95e493be-df40-4c37-83a8-6b695fc143ab.jpg" });
            context.Images.Add(new ImageEntity { Name = "Mark Wahlberg", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/8e1d387b-2037-4733-8c86-64d1b4f2a927.jpg" });
            context.Images.Add(new ImageEntity { Name = "Emma Watson", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/bbb27863-c682-4742-984a-5aa2603026a4.jpg" });
            context.Images.Add(new ImageEntity { Name = "Amy Adams", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/88cda97f-21e4-49ac-a813-1518c3e95e06.jpg" });
            context.Images.Add(new ImageEntity { Name = "Bill Pullman", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/3565ebe4-6fe4-436b-a956-42f06cf0dde0.jpg" });
            context.Images.Add(new ImageEntity { Name = "Bradley Cooper", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/2e109228-3942-4241-83fc-37ba10e3d62a.jpg" });
            context.Images.Add(new ImageEntity { Name = "Elle Fanning", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/2538bb36-0886-4034-8b07-36579e81541f.jpg" });
            context.Images.Add(new ImageEntity { Name = "Jackie Chan", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/bc0d9efc-352c-4987-ae8f-ad2394d28571.jpg" });
            context.Images.Add(new ImageEntity { Name = "Jeremy Renner", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/70c128a1-4f81-4ea8-a676-d6858ecae075.jpg" });
            context.Images.Add(new ImageEntity { Name = "Ryan Reynolds", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/b0028866-c6a0-4ae3-8b5a-699e26c65b03.jpg" });
            context.Images.Add(new ImageEntity { Name = "Jacob Elordi", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/bc46f1b2-89ed-4cbe-9615-056d06dba64e.jpg" });
            context.Images.Add(new ImageEntity { Name = "Keira Knightley", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/7fd0ac84-0321-45cb-8ebf-5673fad95f6c.jpg" });
            context.Images.Add(new ImageEntity { Name = "Kanchan", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/5dbc87fd-e683-46ec-86c2-fc5893623104.jpg" });
            context.Images.Add(new ImageEntity { Name = "Paul Walker", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/4242f2b3-fdce-42a4-9b67-b57f713c1804.jpg" });
            context.Images.Add(new ImageEntity { Name = "Gaspard Ulliel", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/3cc71be0-1d69-4cb4-84a4-c51099162238.jpg" });
            context.Images.Add(new ImageEntity { Name = "James Rittinger", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/4d54ae0e-e7a3-45a7-89f4-dda3cda6bbbe.jpg" });
            context.Images.Add(new ImageEntity { Name = "Rebecca Ferguson", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/19be8405-0f47-413d-9909-3dc547b25135.jpg" });
            context.Images.Add(new ImageEntity { Name = "Idris Elba", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/16f9d28f-e128-4e3d-8be5-544a158cc745.jpg" });
            context.Images.Add(new ImageEntity { Name = "Annabelle Wallis", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/593673d7-9fe0-495e-a62f-544c6af152fe.jpg" });
            context.Images.Add(new ImageEntity { Name = "Bruce Willis", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/97f0e8cd-0db4-496b-a1f1-12bdedc6d630.jpg" });
            context.Images.Add(new ImageEntity { Name = "Pen�lope Cruz", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/19a1934a-00bb-4ff1-adb7-907c68cc8ece.jpg" });
            context.Images.Add(new ImageEntity { Name = "Christina Ricci", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/94bd1907-c727-4e06-82e4-2e34d78a9e7b.jpg" });
            context.Images.Add(new ImageEntity { Name = "Milla Jovovich", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/929740fd-f6f5-4594-931e-9cc44ad8daf1.jpg" });
            context.Images.Add(new ImageEntity { Name = "Shailene Woodley", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/a4e03e58-cdbb-4532-ae5a-e7e28b2968e9.jpg" });
            context.Images.Add(new ImageEntity { Name = "Owen Wilson", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/0c95c40b-cb60-4af4-9a65-b09446b57839.jpg" });
            context.Images.Add(new ImageEntity { Name = "Lily James", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/300387dd-3e81-4d59-b7f4-dfa1fabfeb07.jpg" });
            context.Images.Add(new ImageEntity { Name = "Kevin Spacey", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/761d2282-737e-4ad3-9efa-edf4ffdc8b7e.jpg" });
            context.Images.Add(new ImageEntity { Name = "Esther Acebo", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/1b37ef28-3083-4040-9860-c1388c6ee56c.jpg" });
            context.Images.Add(new ImageEntity { Name = "Angela Bundalovic", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/2666b1e8-2829-425f-afea-c69d26fcc9e5.jpg" });
            context.Images.Add(new ImageEntity { Name = "Connie Nielsen", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/796e2048-543c-4b80-aa78-f736f485d472.jpg" });
            context.Images.Add(new ImageEntity { Name = "Robin Williams", Url = "https://jnsntraineeship2018.blob.core.windows.net/whoiswho/pictures/b5ad0110-47fe-4a8c-943e-f71ba3b3aeb1.jpg" });

        }
    }
}
