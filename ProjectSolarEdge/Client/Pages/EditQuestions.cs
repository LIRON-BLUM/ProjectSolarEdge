using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using System.Linq;
using System.Collections.Generic;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditQuestions : ComponentBase, IDisposable
    {

        [Parameter]
        public string Id { get; set; }

        public int CheckedAnswerID { get; set; }

        

        public bool CheckedAnswerIsRight { get; set; }

        public Question QuestionsCRUD { get; set; } = new Question();

        public IEnumerable<Question> QuestionsData { get; set; }

        public QuestionAnswer Answer { get; set; } = new QuestionAnswer();

        public Subject OneSubject { get; set; } = new Subject();

      
        public SubjectsQuestions QuestionSubject { get; set; } = new SubjectsQuestions();

        public IEnumerable<Subject> SubjectsData { get; set; }

        public IEnumerable<SubjectsQuestions> SubjectsQuestionData { get; set; }

        public SubjectsQuestions subjectNameList { get; set; }
        public int SubjectsQuestionsID { get; set; }

     

        //public IEnumerable<SubjectsQuestions> SQConnection { get; set; } = new List<SubjectsQuestions>();

        public IEnumerable<string> SelectedSubjects { get; set; } = new HashSet<string>();

        public SubjectsQuestionsConnection SubjectConnection { get; set; }
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


            int.TryParse(Id, out var GId);
            
            if (GId == 0) //new Game is being created
            {
                //add some defaults
                QuestionsCRUD = new Question { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Type = (QuestionType)1, Difficulty = (QuestionDifficulty)1 };
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
           
            }


   
            QuestionsCRUD = await QuestionDataService.GetQuestionByIdAsync(int.Parse(Id));
            SelectedSubjects = new HashSet<string>(QuestionsCRUD.Subjects.Select(s => s.SubjectName));
            

            foreach (var myanswer in QuestionsCRUD.Answers)
            {
                if (myanswer.IsRight == true)
                {
                    CheckedAnswerID = myanswer.ID;
                }
            }



        }

        protected async Task AddSubject()
        {
            await QuestionDataService.AddSubjectToDB(OneSubject);
        }

        protected async Task AddAndUpdate()
        {
            List<Subject> selectedSubjectToUpdate = new List<Subject>();

            // Delete the existing subjects 

            foreach (var item in SelectedSubjects)
            {


                Subject s = SubjectsData.Where(s => s.SubjectName == item).SingleOrDefault();
                selectedSubjectToUpdate.Add(s);
                //await QuestionDataService.AddSubjectConnection(new SubjectsQuestionsConnection() { QuestionID = QuestionsCRUD.ID, SubjectID = s.ID });
                Subject newS = SubjectsData.Where(newS => newS.SubjectName == item).SingleOrDefault();
                selectedSubjectToUpdate.Add(newS);
              
              
                await QuestionDataService.AddSubjectConnection(new SubjectsQuestionsConnection() { QuestionID = QuestionsCRUD.ID, SubjectID = newS.ID });

              }

         

            //foreach (var s in SubjectConnectionData)
            //{
            //    Subject DeleteS = SubjectsData.Where(DeleteS => DeleteS.SubjectName == item).SingleOrDefault();
            //    if (item != DeleteS.SubjectName)
            //    {
            //        await QuestionDataService.DeleteSubjectConnection(s.ID);
            //    }
            //}




            QuestionsCRUD.Subjects = selectedSubjectToUpdate;

            foreach (var s in selectedSubjectToUpdate)
            {

                await QuestionDataService.AddSubjectConnection(new SubjectsQuestionsConnection() { QuestionID = QuestionsCRUD.ID, SubjectID = s.ID });
            }


       

            //foreach (var s in selectedSubjectToUpdate)
            //{
                
            //  await QuestionDataService.AddSubjectConnection(new SubjectsQuestionsConnection() { QuestionID = QuestionsCRUD.ID, SubjectID = s.ID});
            //}


            //foreach (var sub in selectedSubjectToUpdate)
            //{
            //    foreach (var s in SubjectConnectionData)
            //    {
            //        if (sub.ID != s.SubjectID)
            //        {

            //            s.SubjectID = sub.ID;
            //            s.QuestionID = QuestionsCRUD.ID;
            //            await QuestionDataService.AddSubjectConnection(s);

            //        }
            //    }

            //}


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

            await QuestionDataService.DeleteQuestion(QuestionsCRUD);


            foreach (var ans in QuestionsCRUD.Answers)
            {


                await QuestionDataService.DeleteAnswer(ans);
                //await QuestionDataService.DeleteQuestionAnswers(ans.QuestionID);

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
            data = data.Where(question =>

            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (question.Creator.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (question.QuestionBody.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if ($"{question.Difficulty} {question.Type} {question.UpdateDate}".Contains(searchString))
                    return true;
                return false;
            }).ToArray();
            totalItems = data.Count();
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
