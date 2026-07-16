namespace UtilisateurPhp
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.pnlAccent = new System.Windows.Forms.Panel();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgUtilisateur = new System.Windows.Forms.DataGridView();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUtilisateur)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1224, 100);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 19F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(35, 20);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(421, 51);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Gestion des utilisateurs";
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Controls.Add(this.pnlAccent);
            this.pnlCard.Controls.Add(this.lblCardTitle);
            this.pnlCard.Controls.Add(this.label);
            this.pnlCard.Controls.Add(this.txtNom);
            this.pnlCard.Controls.Add(this.label2);
            this.pnlCard.Controls.Add(this.txtPrenom);
            this.pnlCard.Controls.Add(this.label3);
            this.pnlCard.Controls.Add(this.txtAge);
            this.pnlCard.Controls.Add(this.btnAjouter);
            this.pnlCard.Controls.Add(this.btnModifier);
            this.pnlCard.Controls.Add(this.btnSupprimer);
            this.pnlCard.Location = new System.Drawing.Point(32, 128);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(348, 520);
            this.pnlCard.TabIndex = 1;
            // 
            // pnlAccent
            // 
            this.pnlAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(182)))), ((int)(((byte)(212)))));
            this.pnlAccent.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAccent.Location = new System.Drawing.Point(0, 0);
            this.pnlAccent.Name = "pnlAccent";
            this.pnlAccent.Size = new System.Drawing.Size(6, 520);
            this.pnlAccent.TabIndex = 0;
            // 
            // lblCardTitle
            // 
            this.lblCardTitle.AutoSize = true;
            this.lblCardTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblCardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(29)))), ((int)(((byte)(149)))));
            this.lblCardTitle.Location = new System.Drawing.Point(28, 22);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(168, 36);
            this.lblCardTitle.TabIndex = 1;
            this.lblCardTitle.Text = "Informations";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.label.Location = new System.Drawing.Point(28, 68);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(57, 25);
            this.label.TabIndex = 2;
            this.label.Text = "NOM";
            // 
            // txtNom
            // 
            this.txtNom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNom.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtNom.Location = new System.Drawing.Point(28, 88);
            this.txtNom.Multiline = true;
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(292, 34);
            this.txtNom.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.label2.Location = new System.Drawing.Point(28, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "PRENOM";
            // 
            // txtPrenom
            // 
            this.txtPrenom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrenom.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtPrenom.Location = new System.Drawing.Point(28, 158);
            this.txtPrenom.Multiline = true;
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(292, 34);
            this.txtPrenom.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.label3.Location = new System.Drawing.Point(28, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "AGE";
            // 
            // txtAge
            // 
            this.txtAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAge.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtAge.Location = new System.Drawing.Point(28, 228);
            this.txtAge.Multiline = true;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(292, 34);
            this.txtAge.TabIndex = 7;
            // 
            // btnAjouter
            // 
            this.btnAjouter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAjouter.FlatAppearance.BorderSize = 0;
            this.btnAjouter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouter.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnAjouter.ForeColor = System.Drawing.Color.White;
            this.btnAjouter.Location = new System.Drawing.Point(28, 300);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(292, 46);
            this.btnAjouter.TabIndex = 8;
            this.btnAjouter.Text = "＋   Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = false;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnModifier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModifier.FlatAppearance.BorderSize = 0;
            this.btnModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifier.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnModifier.ForeColor = System.Drawing.Color.White;
            this.btnModifier.Location = new System.Drawing.Point(28, 358);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(292, 46);
            this.btnModifier.TabIndex = 9;
            this.btnModifier.Text = "  Modifier";
            this.btnModifier.UseVisualStyleBackColor = false;
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(63)))), ((int)(((byte)(94)))));
            this.btnSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSupprimer.FlatAppearance.BorderSize = 0;
            this.btnSupprimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupprimer.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSupprimer.ForeColor = System.Drawing.Color.White;
            this.btnSupprimer.Location = new System.Drawing.Point(28, 416);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(292, 46);
            this.btnSupprimer.TabIndex = 10;
            this.btnSupprimer.Text = "   Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = false;
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgUtilisateur);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Location = new System.Drawing.Point(400, 128);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(18);
            this.pnlGrid.Size = new System.Drawing.Size(792, 520);
            this.pnlGrid.TabIndex = 2;
            // 
            // dgUtilisateur
            // 
            this.dgUtilisateur.AllowUserToAddRows = false;
            this.dgUtilisateur.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dgUtilisateur.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgUtilisateur.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgUtilisateur.BackgroundColor = System.Drawing.Color.White;
            this.dgUtilisateur.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgUtilisateur.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgUtilisateur.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.dgUtilisateur.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgUtilisateur.ColumnHeadersHeight = 46;
            this.dgUtilisateur.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(233)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(29)))), ((int)(((byte)(149)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgUtilisateur.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgUtilisateur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUtilisateur.EnableHeadersVisualStyles = false;
            this.dgUtilisateur.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.dgUtilisateur.Location = new System.Drawing.Point(18, 64);
            this.dgUtilisateur.Name = "dgUtilisateur";
            this.dgUtilisateur.ReadOnly = true;
            this.dgUtilisateur.RowHeadersVisible = false;
            this.dgUtilisateur.RowHeadersWidth = 62;
            this.dgUtilisateur.RowTemplate.Height = 42;
            this.dgUtilisateur.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUtilisateur.Size = new System.Drawing.Size(756, 438);
            this.dgUtilisateur.TabIndex = 1;
            this.dgUtilisateur.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUtilisateur_CellContentClick);
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(29)))), ((int)(((byte)(149)))));
            this.lblGridTitle.Location = new System.Drawing.Point(18, 18);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Padding = new System.Windows.Forms.Padding(4, 0, 0, 14);
            this.lblGridTitle.Size = new System.Drawing.Size(756, 46);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "Liste des utilisateurs";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1224, 680);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion des utilisateurs";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUtilisateur)).EndInit();
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Dessine un dégradé horizontal (violet vers cyan) dans l'en-tête.
        /// </summary>
        private void pnlHeader_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var panel = (System.Windows.Forms.Panel)sender;
            if (panel.ClientRectangle.Width <= 0 || panel.ClientRectangle.Height <= 0)
                return;

            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                panel.ClientRectangle,
                System.Drawing.Color.FromArgb(124, 58, 237),
                System.Drawing.Color.FromArgb(6, 182, 212),
                System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, panel.ClientRectangle);
            }
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlAccent;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgUtilisateur;
    }
}
