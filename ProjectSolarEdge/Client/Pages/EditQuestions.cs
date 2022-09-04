using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using System.Linq;
using System.Collections.Generic;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Client.Shared;
using System.IO;

using System.Net.Http.Json;
using System.Security.Cryptography;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditQuestions : ComponentBase, IDisposable
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
                //add some defaults
                QuestionsCRUD = new Question { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Type = (QuestionType)1, Difficulty = (QuestionDifficulty)1, Feedback = "", Creator = CreatorName, QuestionImagePath = "./Files/QuestionDefaltImage.png" };
                QuestionsCRUD.Answers = new List<QuestionAnswer>();
                QuestionsCRUD.Subjects = new List<Subject>();

                //QuestionImage = "./Files/QuestionDefaltImage.png";

                //ADD ANSWERS
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    //ID = 1,
                    //AnswerBody = " ",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = true
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    //ID = 2,
                    //AnswerBody = " ",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    //ID = 3,
                    //AnswerBody = " ",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    //ID = 4,
                    //AnswerBody = " ",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
            }
            else //Edit existing question
            {
                //Get question by ID
                QuestionsCRUD = await QuestionDataService.GetQuestionByIdAsync(int.Parse(Id));

                QuestionImage = "data:image/png;base64," + QuestionsCRUD.QuestionImagePath;

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

        }
    }
}
