using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditQuestions
    {

        [Parameter]
        public string Id { get; set; }

        public int CheckedAnswerID { get; set; }

        public bool CheckedAnswerIsRight { get; set; }

        public Question QuestionsCRUD { get; set; } = new Question();

        public QuestionAnswer Answer { get; set; } = new QuestionAnswer();


        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {


            int.TryParse(Id, out var QId);

            if (QId == 0) //new Question is being created
            {
                //add some defaults
                QuestionsCRUD = new Question { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Type = (QuestionType)1, Difficulty = (QuestionDifficulty)1 };
                QuestionsCRUD.Answers = new List<QuestionAnswer>();


                //ADD ANSWERS
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                   // ID = 1,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                 //  ID = 2,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                //   ID = 3,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
               //    ID = 4,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
            }
            else //Edit existing question
            {
                //Get question by ID
                QuestionsCRUD = await QuestionDataService.GetQuestionByIdAsync(int.Parse(Id));
            }

          

            QuestionsCRUD = await QuestionDataService.GetQuestionByIdAsync(int.Parse(Id));

            foreach (var TFanswer in QuestionsCRUD.Answers)
            {
                if (TFanswer.IsRight == true)
                {
                    CheckedAnswerID = TFanswer.ID;
                }
            }



        }

        protected async Task AddAndUpdate()
        {
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

                //4) If all successful then navigate the user to edit question or list of questions.
                NavigationManager.NavigateTo("/");

            }
            else
            {
              //  QuestionsCRUD.Answers = new List<QuestionAnswer>();

                await QuestionDataService.UpdateQuestion(QuestionsCRUD);

                foreach (var ans in QuestionsCRUD.Answers)
                {
                   
                    await QuestionDataService.UpdateAnswer(ans);
                  

                }
                
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

                filePicDataUrl = $"data:image/png;base64,{Convert.ToBase64String(fileInArray)}" ;
                //TimeSpan.TicksPerMillisecond


            }

        }

            protected async Task DeleteQuestion()
        {
            await QuestionDataService.DeleteQuestion(QuestionsCRUD.ID);

            foreach (var ans in QuestionsCRUD.Answers)
            {



                await QuestionDataService.DeleteQuestionAnswers(ans.QuestionID);

            }

            NavigationManager.NavigateTo("/");


        }


        string MyQBody = "";
        string Myans = "";

        
        protected async Task OnlyUI()
        {
         

            MyQBody = QuestionsCRUD.QuestionBody + "  " +QuestionsCRUD.Creator +"  "+ QuestionsCRUD.Feedback +"  "+ QuestionsCRUD.Difficulty;

            if (QuestionsCRUD.Answers.Where(a => a.ID == CheckedAnswerID).Count() > 0)
            {
                QuestionsCRUD.Answers.Where(ans => ans.ID == CheckedAnswerID).FirstOrDefault().IsRight = true;
            }

            
            foreach (var ans in QuestionsCRUD.Answers)
            {
				
				Myans += ans.AnswerBody + "-    isRight: "+ ans.IsRight + "    |    ";
               
            } 
        }

      

    }
}
