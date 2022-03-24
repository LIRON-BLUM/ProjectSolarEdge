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
                    ID = 1,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    ID = 2,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    ID = 3,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsRight = false
                });
                QuestionsCRUD.Answers.Add(new QuestionAnswer()
                {
                    ID = 4,
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

        }

        protected async Task AddAndUpdate()
        {
            if (QuestionsCRUD.ID == 0)
            {
                //Get information From User
                QuestionsCRUD.CreationDate = DateTime.Now;
                QuestionsCRUD.UpdateDate = DateTime.Now;
                QuestionsCRUD.Type = (QuestionType)1;
                QuestionsCRUD.Difficulty = (QuestionDifficulty)1;
                QuestionsCRUD.Answers = new List<QuestionAnswer>();
           
                //Add Question to DB
                await QuestionDataService.AddQuestionToDB(QuestionsCRUD);

                //Get new question ID
                QuestionsCRUD = await QuestionDataService.GetQuestionByIdAsync(int.Parse(Id));
                Answer.CreationDate = DateTime.Now;
                Answer.UpdateDate = DateTime.Now;   
                Answer.IsRight=false;

                //Add answers to new Question By QuestionID
                await QuestionDataService.AddAnswerToDB(Answer);
                NavigationManager.NavigateTo("/");

            }
            else
            {
                QuestionsCRUD.Answers = new List<QuestionAnswer>();

                await QuestionDataService.UpdateQuestion(QuestionsCRUD);
                await QuestionDataService.UpdateAnswer(Answer);
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

        protected async Task DeleteQuestion()
        {
            await QuestionDataService.DeleteQuestion(QuestionsCRUD.ID);
            NavigationManager.NavigateTo("/");


        }


        string MyQBody = "";
        string Myans = "";
        bool mychack= true;
        int checkedAnswerID = 0;
        protected async Task OnlyUI()
        {
         

            MyQBody = QuestionsCRUD.QuestionBody + "  " +QuestionsCRUD.Creator +"  "+ QuestionsCRUD.Feedback +"  "+ QuestionsCRUD.Difficulty;
            
            
            foreach (var ans in QuestionsCRUD.Answers)
            {
				
				Myans += ans.AnswerBody +  "    mychack:" +mychack + "isRight"+ ans.IsRight + "    |    ";
               
            } 
        }

      

    }
}
