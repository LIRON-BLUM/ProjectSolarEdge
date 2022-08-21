using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using System.Linq;
using System.Collections.Generic;

namespace ProjectSolarEdge.Client.Pages.GamePages
{ 
    public partial class GameOpeningPage : ComponentBase, IDisposable
    {


        [Parameter]
        public string Id { get; set; }

        public int CheckedAnswerID { get; set; }

        public bool CheckedAnswerIsRight { get; set; }

        public Question QuestionsCRUD { get; set; } = new Question();

        public IEnumerable<Question> QuestionsData { get; set; }

        public IEnumerable<Question> QuestionsDataToDisplay { get; set; }

        public QuestionAnswer Answer { get; set; } = new QuestionAnswer();

        public Subject OneSubject { get; set; } = new Subject();

        public IEnumerable<Subject> SubjectsData { get; set; }


        //public IEnumerable<SubjectsQuestions> SQConnection { get; set; } = new List<SubjectsQuestions>();

        public IEnumerable<string> SelectedSubjects { get; set; } = new HashSet<string>();



        public IEnumerable<SubjectsQuestionsConnection> SubjectConnectionData { get; set; }

        public string DefaultValue { get; set; } = "Select Subject";


        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Question QuestionsToDelete { get; set; } = new Question();





        protected override async Task OnInitializedAsync()
        {



            SubjectsData = await QuestionDataService.GetSubjectsAsync();


            int.TryParse(Id, out var QId);

            if (QId == 0) //new Game is being created
            {
                //add some defaults
                QuestionsCRUD = new Question { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Type = (QuestionType)1, Difficulty = (QuestionDifficulty)1, Feedback = "" };
                QuestionsCRUD.Answers = new List<QuestionAnswer>();
                QuestionsCRUD.Subjects = new List<Subject>();



                //ADD ANSWERS
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    // ID = 1,
                    //AnswerBody = " ",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    //  ID = 2,
                    //AnswerBody = " ",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    //   ID = 3,
                    //AnswerBody = " ",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    //    ID = 4,
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

        private async Task OnSearch(string text)
        {
            QuestionsDataToDisplay = QuestionsData.Where(q => q.QuestionBody.Contains(text) || q.Creator.ToLower().Contains(text.ToLower()));

        }

        protected async Task AddSubject()
        {
            await QuestionDataService.AddSubjectToDB(OneSubject);
        }

        protected async Task AddAndUpdate()
        {


            List<Subject> selectedSubjectToUpdate = new List<Subject>();


            // Delete the existing subjects 
            await QuestionDataService.DeleteSubjectConnection(QuestionsCRUD.ID);






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
              await QuestionDataService.AddQuestionToDB(QuestionsCRUD);

                int QuestionID = QuestionsCRUD.ID;

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

                //4) If all successful then navigate the user to edit question or list of questions.
                NavigationManager.NavigateTo("/Questions");

            }
            else
            {
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
                NavigationManager.NavigateTo("/");
            }

        }





        public string filePicDataUrl { get; set; }

        private async Task OnInputFileChanged(InputFileChangeEventArgs inputFileChangeEvent)
        {

            //get the file
            var file = inputFileChangeEvent.File;

            var fileInArray = new byte[file.Size];

            await file.OpenReadStream(1512000).ReadAsync(fileInArray);

            filePicDataUrl = $"data:image/png;base64,{Convert.ToBase64String(fileInArray)}";




        }


        IList<IBrowserFile> files = new List<IBrowserFile>();
        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                //var file = inputFileChangeEvent.File;


                files.Add(file);
                var fileInArray = new byte[file.Size];
                await file.OpenReadStream(1512000).ReadAsync(fileInArray);

                filePicDataUrl = $"data:image/png;base64,{Convert.ToBase64String(fileInArray)}";
                //TimeSpan.TicksPerMillisecond


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

            NavigationManager.NavigateTo("/");


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

        //[Parameter]
        //public string GameId { get; set; }

        //[Parameter]
        //public string UserId { get; set; }

        //public Game GamePlaying { get; set; }

        //public UsersTable Player { get; set; }
        //public IEnumerable<UserGameScore> TopPlayers { get; set; }

        //[Inject]
        //public IGamesDataService GameDataService { get; set; }

        //[Inject]
        //public IGameAppService GameAppDataService { get; set; }

        //[Inject]
        //public NavigationManager NavigationManager { get; set; }

        //    public void Dispose()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    protected override async Task OnInitializedAsync()
        //    {
        //        GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(GameId));
        //        TopPlayers = await GameAppDataService.GetGameUsersScore(int.Parse(GameId));

        //        Player = new UsersTable()
        //        {
        //            ID = 1,
        //            UserFirstName = "Limor",
        //            UserLastName = "Avrahami",
        //            UserName = "LimorAvrahami",
        //        };


        //        //  Liron - delete this after querise

        //        //TopPlayers = new List<UsersGameRecord>()
        //        //{
        //        //    new UsersGameRecord()
        //        //    {
        //        //        UserFirstName = "Adi",
        //        //        ID =1,
        //        //        UserLastName ="Silagy",
        //        //        UserName ="AdiSilagi",
        //        //        TotalScore=3000
        //        //    },

        //        //    new UsersGameRecord()
        //        //    {
        //        //        UserFirstName = "Moti",
        //        //        ID =2,
        //        //        UserLastName ="Elnekave",
        //        //        UserName ="MotiElnekave",
        //        //        TotalScore=2000
        //        //    },
        //        //      new UsersGameRecord()
        //        //    {
        //        //        UserFirstName = "Liron",
        //        //        ID =3,
        //        //        UserLastName ="Blum",
        //        //        UserName ="LironBlum",
        //        //        TotalScore=2500
        //        //    }
        //        //};

        //        TopPlayers = TopPlayers.OrderByDescending(e => e.UserScore).Take(3);
        //    }


        //    protected async Task STARTGAME()
        //    {
        //        int gameId = GamePlaying.ID;
        //        NavigationManager.NavigateTo($"/GetNextStep/{gameId}");
        //    }

    }
}
