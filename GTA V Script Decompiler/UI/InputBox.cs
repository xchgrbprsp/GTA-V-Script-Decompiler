namespace System.Windows.Forms
{
	/// <summary>
	/// Base in-accessable class for listbox containing methods
	/// This Classes inteneded public methods are accessed through the InputBox Class Below
	/// This class along with InputBox prevents the user accessing the public methods inherited from Form
	/// </summary>
	internal class _InputBox : Form
	{
		#region designercode

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private readonly System.ComponentModel.IContainer components = null;

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
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.label1 = new Label();
			this.textBox1 = new TextBox();
			this.comboBox1 = new ComboBox();
			this.richTextBox1 = new RichTextBox();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor =

					AnchorStyles.Bottom | AnchorStyles.Right;
			this.btnOK.BackColor = Drawing.SystemColors.Control;
			this.btnOK.DialogResult = DialogResult.OK;
			this.btnOK.FlatStyle = FlatStyle.System;
			this.btnOK.Location = new Drawing.Point(131, 103);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new Drawing.Size(59, 29);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "Ok";
			this.btnOK.UseVisualStyleBackColor = false;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor =

					AnchorStyles.Bottom | AnchorStyles.Right;
			this.btnCancel.BackColor = Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.FlatStyle = FlatStyle.System;
			this.btnCancel.Location = new Drawing.Point(209, 103);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Drawing.Size(59, 29);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
#pragma warning disable CA1416 // Validate platform compatibility
			this.label1.Font = new Drawing.Font("Microsoft Sans Serif", 12F, Drawing.FontStyle.Bold,
				Drawing.GraphicsUnit.Point, 0);
#pragma warning restore CA1416 // Validate platform compatibility
			this.label1.Location = new Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Drawing.Size(109, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Enter Value:";
			// 
			// textBox1
			// 
			this.textBox1.Anchor =

					AnchorStyles.Top | AnchorStyles.Left
					  | AnchorStyles.Right;
			this.textBox1.Location = new Drawing.Point(15, 68);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Drawing.Size(253, 20);
			this.textBox1.TabIndex = 3;
			this.textBox1.Visible = false;
			this.textBox1.KeyDown += new KeyEventHandler(this.Input_KeyDown);
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor =

					AnchorStyles.Top | AnchorStyles.Left
					  | AnchorStyles.Right;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new Drawing.Point(15, 67);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new Drawing.Size(252, 21);
			this.comboBox1.TabIndex = 4;
			this.comboBox1.Visible = false;
			this.comboBox1.KeyDown += new KeyEventHandler(this.Input_KeyDown);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new Drawing.Point(15, 68);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new Drawing.Size(252, 142);
			this.richTextBox1.TabIndex = 5;
			this.richTextBox1.Text = "";
			this.richTextBox1.Visible = false;
			// 
			// _InputBox
			// 
			this.AutoScaleDimensions = new Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Drawing.Size(280, 144);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.richTextBox1);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "_InputBox";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.FormClosing += new FormClosingEventHandler(this.InputBox_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button btnOK;
		private Button btnCancel;
		private Label label1;
		private ComboBox comboBox1;
		private TextBox textBox1;

		#endregion

		private RichTextBox richTextBox1;
		private type inputtype;

		private enum type
		{
			text,
			dropdown,
			list
		}

		internal _InputBox()
		{
			InitializeComponent();
			this.Focus();
		}

		internal string Value
		{
			get
			{
				return this.DialogResult == DialogResult.OK
					? (inputtype == type.dropdown ? comboBox1.Text : (inputtype == type.text ? textBox1.Text : null))
					: Default;
			}
		}

		internal string[] ListValue
		{
			get { return this.DialogResult == DialogResult.OK && inputtype == type.list ? richTextBox1.Lines : null; }
		}

		private string Default = null;

		private void InputBox_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.DialogResult is not DialogResult.OK and not DialogResult.Cancel)
				this.DialogResult = DialogResult.Abort;
		}

		internal bool Show(IWin32Window owner, string TitleMessage, string Message, string DefaultResponse)
		{
			textBox1.Visible = true;
			inputtype = type.text;
			textBox1.Focus();
			this.Text = TitleMessage ??"Input Value";
			if (DefaultResponse != null)
			{
				textBox1.Text = DefaultResponse;
				textBox1.SelectAll();
				Default = DefaultResponse;
			}

			label1.Text = Message ??"Enter Value:";
			return owner == null ? ShowDialog() == DialogResult.OK : ShowDialog(owner) == DialogResult.OK;
		}

		internal bool Show(IWin32Window owner, string TitleMessage, string Message, string[] StandardValues)
		{
			comboBox1.Visible = true;
			inputtype = type.dropdown;
			comboBox1.Focus();
			this.Text = TitleMessage ??"Input Value";
			label1.Text = Message ??"Enter Value:";
			comboBox1.Items.Clear();
			comboBox1.Items.AddRange(StandardValues);
			return owner == null ? ShowDialog() == DialogResult.OK : ShowDialog(owner) == DialogResult.OK;
		}

		internal bool Show(IWin32Window owner, string TitleMessage, string Message, AutoCompleteSource acs)
		{
			comboBox1.Visible = true;
			inputtype = type.dropdown;
			comboBox1.Focus();
			this.Text = TitleMessage ??"Input Value";
			label1.Text = Message ??"Enter Value:";
			comboBox1.Items.Clear();
			comboBox1.AutoCompleteSource = acs;
			comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			return owner == null ? ShowDialog() == DialogResult.OK : ShowDialog(owner) == DialogResult.OK;
		}

		internal bool ShowList(IWin32Window owner, string TitleMessage, string Message)
		{
			richTextBox1.Visible = true;
			this.Height = 300;
			inputtype = type.list;
			richTextBox1.Focus();
			this.Text = TitleMessage ??"Input Value";
			label1.Text = Message ??"Enter Value:";
			return owner == null ? ShowDialog() == DialogResult.OK : ShowDialog(owner) == DialogResult.OK;
		}

		private void Input_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.DialogResult = DialogResult.OK;
			}
		}
	}

	/// <summary>
	/// A Class for displaying the user with a box for inputting data
	/// </summary>
	public class InputBox : IDisposable
	{
		private readonly _InputBox _ibx;

		/// <summary>
		/// Initialises a new InputBox Class and enables access to the hidden base class
		/// </summary>
		public InputBox()
		{
			_ibx = new _InputBox();
		}

		public string Value
		{
			get { return _ibx.Value; }
		}

		public string[] ListValue
		{
			get { return _ibx.ListValue; }
		}

		public bool Show(string TitleMessage = null, string Message = null, string DefaulResponse = null,
			IWin32Window owner = null)
		{
			return _ibx.Show(owner, TitleMessage, Message, DefaulResponse);
		}

		public bool Show(string TitleMessage, string Message, string[] StandardValues, IWin32Window owner = null)
		{
			return _ibx.Show(owner, TitleMessage, Message, StandardValues);
		}

		public bool Show(string TitleMessage, string Message, AutoCompleteSource acs, IWin32Window owner = null)
		{
			return _ibx.Show(owner, TitleMessage, Message, acs);
		}

		public bool ShowList(string TitleMessage = null, string Message = null, IWin32Window owner = null)
		{
			return _ibx.ShowList(owner, TitleMessage, Message);
		}

		public static string QuickShow(string TitleMessage, string Message, string[] StandardValues, IWin32Window owner = null)
		{
			_InputBox ibx = new();
			ibx.Show(owner, TitleMessage, Message, StandardValues);
			return ibx.Value;
		}

		public static string QuickShow(string TitleMessage, string Message, AutoCompleteSource acs, IWin32Window owner = null)
		{
			_InputBox ibx = new();
			ibx.Show(owner, TitleMessage, Message, acs);
			return ibx.Value;
		}

		public static string QuickShow(string TitleMessage = null, string Message = null, string DefaultResponse = null,
			IWin32Window owner = null)
		{
			_InputBox ibx = new();
			ibx.Show(owner, TitleMessage, Message, DefaultResponse);
			return ibx.Value;
		}

		public static string[] QuickShowList(string TitleMessage = null, string Message = null, IWin32Window owner = null)
		{
			_InputBox ibx = new();
			ibx.ShowList(owner, TitleMessage, Message);
			return ibx.ListValue;
		}

		/// <summary>
		/// Handle Disposing of the the OpenFileDialog behind this class
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Handle Disposing of the the OpenFileDialog behind this class
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_ibx != null)
					_ibx.Dispose();
			}
		}
	}
}
