using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using System.Linq;
using System.Collections.Generic;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Client.Shared;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditYesNoQuestion : ComponentBase, IDisposable
    {

        [Parameter]
        public string Id { get; set; }


        public UsersTable EditorData { get; set; }

        public int CheckedAnswerID { get; set; }

        public int _selectedDifficulty = 1;

        public string SelectedDiff = "Easy";

        public Question QuestionsCRUD { get; set; } = new Question();


        public IEnumerable<Question> QuestionsData { get; set; }

        public IEnumerable<Question> QuestionsDataToDisplay { get; set; }


        public Subject OneSubject { get; set; } = new Subject();

        public IEnumerable<Subject> SubjectsData { get; set; }


        public IEnumerable<string> SelectedSubjects { get; set; } = new HashSet<string>();


        public string DefaultValue { get; set; } = "Select Subject";


        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        [Inject]
        public IUsersDataService UsersDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Question QuestionsToDelete { get; set; } = new Question();


        public string filePicDataUrl { get; set; }

        public string QuestionImage { get; set; }

        [Inject]
        public Blazored.LocalStorage.ISyncLocalStorageService LocalService { get; set; }

        string EditorIDSessiom = "";

        string CreatorName = "";

        //public List<Subject> selectedSubjectToUpdate = new List<Subject>();
        public string newQuestionIMG { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EditorIDSessiom = LocalService.GetItem<string>("SessionValue");


            int.TryParse(EditorIDSessiom, out var EId);

            EditorData = await UsersDataService.GetUsererByID(EId);

            SubjectsData = await QuestionDataService.GetSubjectsAsync();

            CreatorName = (EditorData.UserFirstName + " " + EditorData.UserLastName);

            int.TryParse(Id, out var QId);

            if (QId == 0) //new Game is being created
            {
                string newQuestionIMG = "iVBORw0KGgoAAAANSUhEUgAAAN4AAAC8CAYAAAAXWxDmAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAApVSURBVHgB7d1NbBTnGcDxd9cckoKwaaU6qlRjk0pRlSa2aXooqRpsRKT2ZHwJlQg2ObSQKnwktnsJ2AZOxgm2q5aPSNgQSyUX26d+BDWGqE1OzRpIhVqFLA4XHIliKORDCp7OM2Rq73pmdnY9s8/a/v8kEliDhSz+fuf9mNmE+dp4KlWxYsWqPTOWtTFh/zAAImEZkyozpv8ntY+ddl9LyH/GU1eqy5LJ8fKVD1evrfyWWfnwQ2ZFWdIAWLjbdz8zU7fuyI/0/ZmZxob6719zwnv34r/SdnDVVXZ0AOLxydRNMzl1047vs/Vl76autFZ+s7x13Xe+bQDEp3zVN2T0W/PlV9aXyUQyuadyzWoDIH4ylTOJsp8m7YlfnZQIIH6yfiKLl6ygAEXkLloSHqCA8AAFhAcoIDxAAeEBCggPUEB4gALCAxQQHqCA8AAFhAcoIDxAAeEBCggPUEB4gALCAxQQHqCA8AAFhAcoIDxAAeEBCggPUEB4gALCAxQQHqCA8AAFhAcoIDxAAeEBCggPUEB4gALCAxQQHqCA8AAFhAcoIDxAAeEBCggPUEB4gALCAxQQHqCA8AAFhAcoIDxAAeEBCggPUEB4gALCAxQQHqCA8AAFhAcoIDxAAeEBCggPULDCwNP59y6a7p5hM/HhVTN9555BfipWrzRNP9tgOtufN9VVlQaZGPE8dB8ZNg1NHXZ8l4iuQPJ1G3rrnFn3o+3m9NlzBpkIL8vQ2bdN15E3DaJhWcbsO3DCXPtkymAW4WXpPzlmEK1b0/81A2+MGsxijpdF5nTZ6n7wqDNnQThymZn9dbx2nRFvLsILYfR0p6n+LgsEYQ3+4W3zwp7XMl6bvs1ceS4uNQEFhAcoIDxAAeEBClhciZmcgBn74/tm0l7Vm75913mt1l4l3fj0k87JDixPhBcT2YiXEzBey+hyIqb/5KizUtq6dbNzrArLC5eaEZNl8x0v9Zodu1/LuXclH++y46z54XZOdiwzhBexhi3tzhnFfEiA8ueIb/kgvAjJpaXXyZcwJL4du3sNlgfCi4iMVn6Hq2UOl/7HGWN9+heTeuf3ZnCgzfMkjMz9zv/9osHSx+JKRGSxxIuEJmc9XfLzuq9XNRua2ufNA2XU3Ph0rcHSxogXEa9LzL2/bMqIbi4Z8QZ/2zb/8/zzY4Olj/Ai4hVey9ZnA/9M3ePr5r0me32c5F/6CC8iXneqV5SvCvwzuT6OpYvwIuK1WHIhx0LJxIfel5UVqwlyqSO8iMhiSTbZHA+6D63vxMi81+Tys6Jc/6Zb+Xv3nxjlPrqYEF5EvOZzfhvj8o9536vHzWmPjfaWXwTPC4vh1vRd5++9d/9xs37Ti2zsx4DthIhs3PCk80P24uaSRZeap7Y7H6uuesT+R3zD95GBcrmqfXA6PXnDNO84+P/ForT9921sbjcjQ52+K7TIHyNehGR7wO/ZLBKkHJwOemTg0cO7VB8xIdE1NnfMW6FN2yNefeOL9h4jT1+LCuFFSKIZHztS0IORjh7eaY92PzZa3OiCtjJkzhomvkTCIAfCi5hcjqXGj4W+LHNiHT1ib7ZvMVpkDpcrOpfEJ6Mf876FIbwYSExyVExGv5atm+eNgPJrWQUdGmhzznB6rYgWy8Tlq3ZIuzyj8xu55VKUuykWhsWVGLkLLmbgwQa7ewd6qTwqUKKTgPwWeuQbx9DZc56XlxJqvb3iOTjwCnfSF4ARr0hk9JB/zKUSnWxlSDhB0cn/u9q3mVF7RdNr9JNvJFtaull0KQDhLUMSXetL3vf+ydzUjc7V9PMNzrzV75uGzPv2vXqMzfY8EN4yI7cd5ROdyx0FnUtnD30nxx7MFZn3hUJ4y4hE53ezbstzm3Nuhbjx+T2cyT2p43cGFbMIr4RFeemWK7qhgM3/bDLvk81+LxKf303BmEV4Jeqac1pkl7N4sdAA5Vyo/2MptjnR5Utu8pUtE97MpTCEV4Lm7q2N/em9gudOlmU5jxrs8xmBZOTqWsAzPYPmhAhGeCVGQsveW3PnTuN/mwj9eeQOg8YtHZ6PGpQjXRJdFA/SdQ8LyEEBhEd4JUQ2q51LS4+9NYmvsfk3prvnTeftjYM40TV3zLtTQkh0rx/aGenTq+VOejmFwxOxwyO8EtHdMxzquZpdvcPm5f3HnMtIL3LYWe6h83oGTMKu7pQdSFznQoM225GJ8EqARNfVG/70h+yZrXuqxYlsrqA7DNbYo9I7Iz2m9bl4Lwlls13utEAwwlMkg1ZQdPKGJvt+5T06Pbj07DCpyx85v84Z3WiP6mFsZOKQtBKJ7gX70tLvfRb22sEdPfRg5Chfvcp025eY2ZeXEtn6Tb82XW3bnM/jFV1N1SNOdKw8lhZGPAWy+BEUnbNBfWj2ck322iQeicjz9/cOE90iQ3hF5q445rvML2ckg+LLRnSljfCKyO+ZJiLMMr9E9MFff5fz/rf6J75HdCWO8IokOLrwy/yyZ1b3uP9jJeqfeJToFgEWV4ogzIpj2Ge0BK2CyuXlCPtoiwIjXsxSlz7yjc6dh4WJLszWwwccWl40GPFi5EbndQQsn8UPie7l/cd9DzvP3XrA4sCIFxM5dxlNdJaz9RB0h0GpRZfrLCkY8WIh0fmdu5QVx5GhA6Gic/f7xv78vufHo7rDAMVHeBELmoe5y/xhFj/c/T6/rYfONqJbzLjUjFCuxY+w0aUnpwKjO8UtOIseI15EnGeaBEQ3OBD+8Qrd9ufxik62Hl4/vDP2OwwQP8KLiNebTIpC5mEXPG5gzXe/D6WNS82I1KzNPEO5kMcrtG59NuMdd/LZ7ysFvFtQbox4ETnV/4ppbj3obJTLPO6AHV2hd3rL3Qhr7VXPM2+dM9VVleaAvZDCxvjSQngRkdFITo7I3Ex+vtBjWzIvbOUBQksW4UXIefutDdzljdyY4yFynFzJjfAABVxqhiDnJS2+jeeBr1UuhJdF5mnZB5vlUXpAlLjUzMIGdTxaOG2TgfCydHY8zwZwxOQAwDM80zMD4WWR7YBT/W3Oc1CwMPIl5Gln3pjjeZCNa3nqcv8bo+biZd7dtBAV5bKnWWu2219LngEzH+H5kO/QRw/yOAXEg0tNQAHhAQoID1BAeIACwgMUEB6ggPAABYQHKCA8QAHhAQoID1BAeIACwgMUEB6ggPAABYQHKCA8QAHhAQoID1BAeIACwgMUEB6ggPAABYQHKCA8QAHhAQoID1BAeIACwgMUEB6ggPAABYQHKCA8QAHhAQoID1BAeIACwgMUEB6ggPAABYQHKCA8QAHhAQoID1BAeIACwgMUEB6ggPAABYQHKCA8QAHhAQoID1BAeIACwgMUOOF9dX/GACiepGUlzt/7/AsDIH7T9z43ljETSWPdvzA5ddMAiN+n/7ltd2f6kjPmoT67wjTxAfGSxm7cupN+pvaxM8mG+prpmZmZRvvF9L+v37BkKAQQDVk/kaYuXb1u2Y19bLe2SV5PzP1N46krrclEcnciYeoMgEhYlnXe/s+FGfNFf0N9/bS89j80LvryNSG2iwAAAABJRU5ErkJggg==";
                //add some defaults
                QuestionsCRUD = new Question { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Type = (QuestionType)1, Difficulty = (QuestionDifficulty)1, Feedback = "", Creator = CreatorName, QuestionImagePath = newQuestionIMG };
                newQuestionIMG = "data:image/png;base64," + QuestionsCRUD.QuestionImagePath;
                QuestionsCRUD.Answers = new List<QuestionAnswer>();
                QuestionsCRUD.Subjects = new List<Subject>();

                //QuestionImage = "./Files/QuestionDefaltImage.png";

                //ADD ANSWERS
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    //ID = 1,
                    AnswerBody = "True ",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = true
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    //ID = 2,
                    AnswerBody = "False ",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
             
            }
            else //Edit existing question
            {
                //Get question by ID
                QuestionsCRUD = await QuestionDataService.GetQuestionByIdAsync(int.Parse(Id));

                QuestionImage = $"data:image/png;base64,{QuestionsCRUD.QuestionImagePath}";

                foreach (var myanswer in QuestionsCRUD.Answers)
                {
                    if (myanswer.IsRight == true)
                    {
                        CheckedAnswerID = myanswer.ID;

                    }
                }
            }





            SelectedSubjects = new HashSet<string>(QuestionsCRUD.Subjects.Select(s => s.SubjectName));

            QuestionsData = await QuestionDataService.GetQuestionsAsync();
            QuestionsDataToDisplay = QuestionsData;



        }

        protected async Task DifficultyChanged(int i)
        {
            if (i == 1)
            {
                SelectedDiff = "Easy";
                QuestionsCRUD.Difficulty = QuestionDifficulty.Easy;
            }
            else if (i == 2)
            {
                SelectedDiff = "Medium";
                QuestionsCRUD.Difficulty = QuestionDifficulty.Medium;
            }
            else
            {
                SelectedDiff = "Hard";
                QuestionsCRUD.Difficulty = QuestionDifficulty.Hard;
            }
        }


        public Question QuestionsToEdit { get; set; } = new Question();



        protected async Task NavMultipleQuestion()
        {
            NavigationManager.NavigateTo("./EditQuestion");
        }

        protected async Task NavYesNoQuestion()
        {
            NavigationManager.NavigateTo("./Questions");
        }

        protected async Task NavOrderQuestion()
        {
            NavigationManager.NavigateTo("./OrderQuestionEdit");
        }

        protected async Task NavigateToEdit(int qid)
        {

            QuestionsToEdit = await QuestionDataService.GetQuestionByIdAsync(qid);

            if (QuestionsToEdit.Type == QuestionType.MultipleChoice)
            {
                NavigationManager.NavigateTo($"./EditQuestion/{qid}");
            }
            else if (QuestionsToEdit.Type == QuestionType.Order)
            {
                NavigationManager.NavigateTo($"./OrderQuestionEdit/{qid}");
            }
            else
            {
                NavigationManager.NavigateTo($"./EditQuestion/{qid}");
            }
        }

        private async Task OnSearch(string text)
        {
            QuestionsDataToDisplay = QuestionsData.Where(q => q.QuestionBody.Contains(text) || q.Creator.ToLower().Contains(text.ToLower()));

        }

        protected async Task AddSubject()
        {
            await QuestionDataService.AddSubjectToDB(OneSubject);
            SubjectsData = await QuestionDataService.GetSubjectsAsync();

        }

        protected async Task DeleteSubject(int subjectID)
        {
            await QuestionDataService.DeleteSubjectByIDFromConnection(subjectID);
            await QuestionDataService.DeleteSubject(subjectID);
            SubjectsData = await QuestionDataService.GetSubjectsAsync();
        }

        protected async Task AddAndUpdate()
        {


            List<Subject> selectedSubjectToUpdate = new List<Subject>();


            //QuestionsCRUD.Subjects = selectedSubjectToUpdate;
            //QuestionsCRUD.Creator = EditorData.UserFirstName + " " + EditorData.UserLastName;
            //  await QuestionDataService.DeleteSubjectConnection(QuestionsCRUD.ID);


            // 1) Check which answer is the correct one and set it up
            foreach (var ans in QuestionsCRUD.Answers)
            {

                ans.IsRight = false;

                if (QuestionsCRUD.Answers.Where(a => a.ID == CheckedAnswerID).Count() > 0)
                {

                    QuestionsCRUD.Answers.Where(ans => ans.ID == CheckedAnswerID).FirstOrDefault().IsRight = true;
                }
            }


            if (QuestionsCRUD.ID == 0) // Create new question
            {



                // 2) Save the question itself into the database and get the question ID back from the database
                int QuestionID = await QuestionDataService.AddQuestionToDB(QuestionsCRUD);

                //if (QuestionID != 0) // Question added to the DB
                //{
                //    QuestionsCRUD.ID = QuestionID;
                //}






                if (QuestionID != 0) // Question added to the DB
                {
                    QuestionsCRUD.ID = QuestionID;
                }

                // 3) Add the answers on the question to the database using the question ID retunred from the DB 
                foreach (var ans in QuestionsCRUD.Answers)
                {
                    ans.QuestionID = QuestionID;
                    await QuestionDataService.AddAnswerToDB(ans);

                }


                foreach (var item in SelectedSubjects)
                {
                    Subject s = SubjectsData.Where(s => s.SubjectName == item).SingleOrDefault();
                    selectedSubjectToUpdate.Add(s);
                    await QuestionDataService.AddSubjectConnection(new SubjectsQuestionsConnection() { QuestionID = QuestionsCRUD.ID, SubjectID = s.ID });
                }
                QuestionsCRUD.Subjects = selectedSubjectToUpdate;
                QuestionsCRUD.QuestionImagePath = QuestionImage;

                //4) If all successful then navigate the user to edit question or list of questions.
                NavigationManager.NavigateTo("./Questions");


            }
            else
            {

                // Delete the existing subjects 
                await QuestionDataService.DeleteSubjectConnection(QuestionsCRUD.ID);
                //  QuestionsCRUD.Answers = new List<QuestionAnswer>();

                await QuestionDataService.UpdateQuestion(QuestionsCRUD);

                foreach (var ans in QuestionsCRUD.Answers)
                {



                    await QuestionDataService.UpdateAnswer(ans);


                }

                foreach (var item in SelectedSubjects)
                {
                    Subject s = SubjectsData.Where(s => s.SubjectName == item).SingleOrDefault();
                    selectedSubjectToUpdate.Add(s);
                    await QuestionDataService.AddSubjectConnection(new SubjectsQuestionsConnection() { QuestionID = QuestionsCRUD.ID, SubjectID = s.ID });
                }
                QuestionsCRUD.Subjects = selectedSubjectToUpdate;
                NavigationManager.NavigateTo("./Questions");
            }

        }


        public int MaxAlloedFiles = int.MaxValue;
        public long maxFileSize = long.MaxValue;
        public List<string> fileNames = new();

        private async Task UploadQuestionFile(InputFileChangeEventArgs e)
        {
            var imageFiles = e.GetMultipleFiles();
            foreach (var file in imageFiles)
            {
                if (file.Size <= maxFileSize)
                {
                    var buffer = new byte[file.Size];
                    await file.OpenReadStream(maxFileSize).ReadAsync(buffer);
                    var imageBase64 = Convert.ToBase64String(buffer);

                    QuestionsCRUD.QuestionImagePath = imageBase64;

                    var showfile = imageBase64;
                    filePicDataUrl = $"data:image/png;base64,{showfile}";

                    QuestionImage = filePicDataUrl;

                }
            }
        }





        protected async Task DeleteQuestion()
        {

            await QuestionDataService.DeleteQuestion(QuestionsCRUD);


            foreach (var ans in QuestionsCRUD.Answers)
            {


                await QuestionDataService.DeleteAnswer(ans);
                //await QuestionDataService.DeleteQuestionAnswers(ans.QuestionID);

            }

            NavigationManager.NavigateTo("./Questions");


        }







        protected async Task QuestionsToDeleteID(int id)
        {
            QuestionsToDelete = await QuestionDataService.GetQuestionByIdAsync(id);

            DeleteTheQuestion();

        }
        protected async Task DeleteTheQuestion()
        {

            await QuestionDataService.DeleteQuestion(QuestionsToDelete);





        }

        bool fixed_header = true;
        bool fixed_footer = false;

        //private string searchString = "";
        private string searchString = "";
        private int totalItems;
        private IEnumerable<Question> pagedData;
        private MudTable<Question> table;
        public string SubName = "";

        //private bool FilterFunc(Question question)

        protected async Task<TableData<Question>> ServerReload(TableState state)
        {

            IEnumerable<Question> data = await QuestionDataService.GetQuestionsAsync();
            //await Task.Delay(300);

            switch (state.SortLabel)
            {
                case "ID_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.ID);
                    break;
                case "Type_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Type);
                    break;
                case "Difficulty_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Difficulty);
                    break;
                case "Creator_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Creator);
                    break;
                case "Creation_Date_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.CreationDate);
                    break;
            }

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<Question>() { TotalItems = totalItems, Items = pagedData };

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
