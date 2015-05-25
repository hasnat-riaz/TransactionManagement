using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using DBAccessFromApplication.DAL.DAO;
using DBAccessFromApplication.DAL.Gateway;

namespace DBAccessFromApplication
{
    public partial class TraineeEntryUI : Form
    {
        public TraineeEntryUI()
        {
            InitializeComponent();
            this.PopulateTakenCoursesListView();
        }

        private void PopulateTakenCoursesListView()
        {
            CourseGateway courseGateway = new CourseGateway();
            List<Course> courses =  courseGateway.GetCourseList();
            foreach (Course course in courses)
            {
                
                ListViewItem item = new ListViewItem();
                item.Tag = course;
                item.Text = course.Id.ToString();
                item.SubItems.Add(course.Title);
                takenCoursesListView.Items.Add(item);
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string messageForUser;

            Trainee traineeObj = new Trainee(idTextBox.Text, nameTextBox.Text);
            TraineeGateway traineeGatewayObj = new TraineeGateway();
            
            List<Course> selectedCourseList = new List<Course>();

            foreach (ListViewItem item in takenCoursesListView.CheckedItems)
            {
                selectedCourseList.Add((Course)item.Tag);
            }
            try
            {
                messageForUser = traineeGatewayObj.SaveTraineeWithTakenCourses(traineeObj, selectedCourseList);
            }
            catch (Exception exObj)
            {
                messageForUser = exObj.Message;
            }
            MessageBox.Show(messageForUser);
        }

        private void TraineeEntryUI_Load(object sender, EventArgs e)
        {
            
        }

    }
}

