namespace TwainDirect.OnTwain
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_listviewTasks = new System.Windows.Forms.ListView();
            this.Task = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // m_listviewTasks
            // 
            this.m_listviewTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listviewTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Task});
            this.m_listviewTasks.Location = new System.Drawing.Point(12, 12);
            this.m_listviewTasks.MultiSelect = false;
            this.m_listviewTasks.Name = "m_listviewTasks";
            this.m_listviewTasks.Size = new System.Drawing.Size(271, 305);
            this.m_listviewTasks.TabIndex = 1;
            this.m_listviewTasks.UseCompatibleStateImageBehavior = false;
            // 
            // Task
            // 
            this.Task.Text = "Task";
            this.Task.Width = 1000;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 358);
            this.Controls.Add(this.m_listviewTasks);
            this.Name = "MainForm";
            this.Text = "TWAIN Direct on TWAIN";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView m_listviewTasks;
        private System.Windows.Forms.ColumnHeader Task;
    }
}

