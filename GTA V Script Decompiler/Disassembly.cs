using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace Decompiler
{
    public partial class Disassembly : Form
    {
        public static string[][] OpcodeArgs
= {
            new[]{"NOP", ""},
            new[]{"IADD", ""},
            new[]{"ISUB", ""},
            new[]{"IMUL", ""},
            new[]{"IDIV", ""},
            new[]{"IMOD", ""},
            new[]{"INOT", ""},
            new[]{"INEG", ""},
            new[]{"IEQ", ""},
            new[]{"INE", ""},
            new[]{"IGT", ""},
            new[]{"IGE", ""},
            new[]{"ILT", ""},
            new[]{"ILE", ""},
            new[]{"FADD", ""},
            new[]{"FSUB", ""},
            new[]{"FMUL", ""},
            new[]{"FDIV", ""},
            new[]{"FMOD", ""},
            new[]{"FNEG", ""},
            new[]{"FEQ", ""},
            new[]{"FNE", ""},
            new[]{"FGT", ""},
            new[]{"FGE", ""},
            new[]{"FLT", ""},
            new[]{"FLE", ""},
            new[]{"VADD", ""},
            new[]{"VSUB", ""},
            new[]{"VMUL", ""},
            new[]{"VDIV", ""},
            new[]{"VNEG", ""},
            new[]{"IAND", ""},
            new[]{"IOR", ""},
            new[]{"IXOR", ""},
            new[]{"I2F", ""},
            new[]{"F2I", ""},
            new[]{"F2V", ""},
            new[]{"PUSH_CONST_U8", "b"},
            new[]{"PUSH_CONST_U8_U8", "bb"},
            new[]{"PUSH_CONST_U8_U8_U8", "bbb"},
            new[]{"PUSH_CONST_U32", "d"},
            new[]{"PUSH_CONST_F", "f"},
            new[]{"DUP", ""},
            new[]{"DROP", ""},
            new[]{"NATIVE", "bbb"},
            new[]{"ENTER", ""},
            new[]{"LEAVE", "bb"},
            new[]{"LOAD", ""},
            new[]{"STORE", ""},
            new[]{"STORE_REV", ""},
            new[]{"LOAD_N", ""},
            new[]{"STORE_N", ""},
            new[]{"ARRAY_U8", "b"},
            new[]{"ARRAY_U8_LOAD", "b"},
            new[]{"ARRAY_U8_STORE", "b"},
            new[]{"LOCAL_U8", "b"},
            new[]{"LOCAL_U8_LOAD", "b"},
            new[]{"LOCAL_U8_STORE", "b"},
            new[]{"STATIC_U8", "b"},
            new[]{"STATIC_U8_LOAD", "b"},
            new[]{"STATIC_U8_STORE", "b"},
            new[]{"IADD_U8", "b"},
            new[]{"IMUL_U8", "b"},
            new[]{"IOFFSET", ""},
            new[]{"IOFFSET_U8", "b"},
            new[]{"IOFFSET_U8_LOAD", "b"},
            new[]{"IOFFSET_U8_STORE", "b"},
            new[]{"PUSH_CONST_S16", "s"},
            new[]{"IADD_S16", "s"},
            new[]{"IMUL_S16", "s"},
            new[]{"IOFFSET_S16", "s"},
            new[]{"IOFFSET_S16_LOAD", "s"},
            new[]{"IOFFSET_S16_STORE", "s"},
            new[]{"ARRAY_U16", "h"},
            new[]{"ARRAY_U16_LOAD", "h"},
            new[]{"ARRAY_U16_STORE", "h"},
            new[]{"LOCAL_U16", "h"},
            new[]{"LOCAL_U16_LOAD", "h"},
            new[]{"LOCAL_U16_STORE", "h"},
            new[]{"STATIC_U16", "h"},
            new[]{"STATIC_U16_LOAD", "h"},
            new[]{"STATIC_U16_STORE", "h"},
            new[]{"GLOBAL_U16", "h"},
            new[]{"GLOBAL_U16_LOAD", "h"},
            new[]{"GLOBAL_U16_STORE", "h"},
            new[]{"J", "R"},
            new[]{"JZ", "R"},
            new[]{"IEQ_JZ", "R"},
            new[]{"INE_JZ", "R"},
            new[]{"IGT_JZ", "R"},
            new[]{"IGE_JZ", "R"},
            new[]{"ILT_JZ", "R"},
            new[]{"ILE_JZ", "R"},
            new[]{"CALL", "a"},
            new[]{"GLOBAL_U24", "a"},
            new[]{"GLOBAL_U24_LOAD", "a"},
            new[]{"GLOBAL_U24_STORE", "a"},
            new[]{"PUSH_CONST_U24", "a"},
            new[]{"SWITCH", ""},
            new[]{"STRING", ""},
            new[]{"STRINGHASH", ""},
            new[]{"TEXT_LABEL_ASSIGN_STRING", "b"},
            new[]{"TEXT_LABEL_ASSIGN_INT", "b"},
            new[]{"TEXT_LABEL_APPEND_STRING", "b"},
            new[]{"TEXT_LABEL_APPEND_INT", "b"},
            new[]{"TEXT_LABEL_COPY", ""},
            new[]{"CATCH", ""},
            new[]{"THROW", ""},
            new[]{"CALLINDIRECT", ""},
            new[]{"PUSH_CONST_M1", ""},
            new[]{"PUSH_CONST_0", ""},
            new[]{"PUSH_CONST_1", ""},
            new[]{"PUSH_CONST_2", ""},
            new[]{"PUSH_CONST_3", ""},
            new[]{"PUSH_CONST_4", ""},
            new[]{"PUSH_CONST_5", ""},
            new[]{"PUSH_CONST_6", ""},
            new[]{"PUSH_CONST_7", ""},
            new[]{"PUSH_CONST_FM1", ""},
            new[]{"PUSH_CONST_F0", ""},
            new[]{"PUSH_CONST_F1", ""},
            new[]{"PUSH_CONST_F2", ""},
            new[]{"PUSH_CONST_F3", ""},
            new[]{"PUSH_CONST_F4", ""},
            new[]{"PUSH_CONST_F5", ""},
            new[]{"PUSH_CONST_F6", ""},
            new[]{"PUSH_CONST_F7", ""},
            new[]{"IS_BIT_SET", ""}
        };

        Function Function;

        public Disassembly(Function func)
        {
            InitializeComponent();
            Function = func;
            fctb1.Text = DisassembleFunction();
        }

        // TODO: Rewrite this
        static string DisassembleInstruction(HLInstruction instruction)
        {
            string bytes = "";

            bytes += ((uint)instruction.OriginalInstruction).ToString("X").PadLeft(2, '0');

            int i = 0;
            foreach (var op in instruction.operands)
            {
                bytes += " " + ((sbyte)op).ToString("X").PadLeft(2, '0');

                i++;

                if (i > 4)
                {
                    bytes += "...";
                    break;
                }
            }

            bytes = bytes.PadRight(16);

            bytes += " " + instruction.OriginalInstruction.ToString();

            var args = OpcodeArgs[(int)instruction.OriginalInstruction];

            switch (args[1])
            {
                case "b":
                    bytes += " " + (int)instruction.GetOperand(0);
                    break;
                case "bb":
                    bytes += " " + (int)instruction.GetOperand(0) + ", " + (int)instruction.GetOperand(1);
                    break;
                case "bbb":
                    bytes += " " + (int)instruction.GetOperand(0) + ", " + (int)instruction.GetOperand(1) + ", " + (int)instruction.GetOperand(2);
                    break;
                case "d":
                case "h":
                case "s":
                case "R":
                case "a":
                    bytes += " " + instruction.GetOperandsAsInt;
                    break;
                case "f":
                    bytes += " " + instruction.GetFloat;
                    break;
            }

            return bytes;
        }

        string DisassembleFunction()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var ins in Function.Instructions)
            {
                sb.AppendLine(DisassembleInstruction(ins));
            }

            return sb.ToString();
        }

        int GetNumPatternResults(string pat)
        {
            var bytes = pat.Split(" ");
            int num = 0;

            var compiled = new List<int>();

            foreach (var @byte in bytes)
            {
                if (int.TryParse(@byte, System.Globalization.NumberStyles.HexNumber, null, out int res))
                    compiled.Add(res);
                else
                    compiled.Add(-1);
            }

            for (int i = 0; i < (Function.ScriptFile.CodeTable.Count - compiled.Count); i++)
            {
                for (int j = 0; j < bytes.Length; j++)
                {
                    if (compiled[j] == -1)
                        continue;

                    if (compiled[j] != Function.ScriptFile.CodeTable[i+j])
                        goto fail;
                }
                num++;
fail:
                continue;
            }

            return num;
        }

        private void checkPatternUniquenessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox IB = new InputBox();

            while (true)
            {
                if (!IB.Show("Pattern", "Enter a pattern"))
                    return;

                int results = GetNumPatternResults(IB.Value);

                if (results == 0)
                    MessageBox.Show("Cannot find pattern");
                else if (results == 1)
                    MessageBox.Show("Pattern is unique");
                else
                    MessageBox.Show($"Pattern is not unique, {results} matches found!");
            }
        }

        private void createPatternAtCursorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pattern = "";
            int offset = fctb1.Selection.Start.iLine;

            for (int i = offset; i < Function.Instructions.Count; i++)
            {
                if (Function.Instructions[i].Instruction == Instruction.SWITCH)
                    break;

                pattern += ((uint)Function.Instructions[i].OriginalInstruction).ToString("X").PadLeft(2, '0');

                foreach (var _ in Function.Instructions[i].operands)
                {
                    pattern += " ?";
                }

                pattern += " ";

                int results = GetNumPatternResults(pattern);

                if (results == 0)
                    break;
                else if (results == 1)
                {
                    var option = MessageBox.Show($"The pattern is {pattern}{Environment.NewLine}Copy to clipboard?", "Pattern Found", MessageBoxButtons.YesNo);
                    if (option == DialogResult.Yes)
                        Clipboard.SetText(pattern);

                    return;
                }
            }

            MessageBox.Show("Cannot find pattern");
        }
    }
}
