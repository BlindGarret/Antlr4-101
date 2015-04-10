using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using StarterProject.Annotations;
using StarterProject.Listeners;

namespace StarterProject
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties

        public String Input
        {
            get { return _input; }
            set
            {
                if (_input != value)
                {
                    _input = value;
                    OnPropertyChanged("Input");
                }
            }
        }

        private String _input;

        public string Output
        {
            get { return _output; }
            set
            {
                if (_output != value)
                {
                    _output = value;
                    OnPropertyChanged("Output");
                }
            }
        }
        private string _output;

        #endregion

        #region Parse Command

        public ICommand ParseCommand
        {
            get { return _parseCommand; }
        }

        private ICommand _parseCommand;

        private void OnParseCommandExecuted(object obj)
        {
            //create tree

            input = new AntlrInputStream(Input);
            lexer = new ArrayInitLexer(input);
            tokens = new CommonTokenStream(lexer);
            parser = new ArrayInitParser(tokens);
            var tree = parser.init();

            //create and use walker
            var walker = new ParseTreeWalker();
            var listener = new ShortToUnicodeStringListener();
            var builder = new StringBuilder();
            //this is a memory leak VVVV but it's just a test app so who cares.
            listener.InitEntered += (e) => builder.Append("\"" );
            listener.InitExited += (e) => builder.Append("\"" );
            listener.ValueEntered += (s,e) => builder.Append(s);
            walker.Walk(listener, tree);
            Output = string.Concat(tree.ToStringTree(parser), Environment.NewLine, builder.ToString());
        }

        #endregion

        #region Property changed

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private AntlrInputStream input;
        private ArrayInitLexer lexer;
        private CommonTokenStream tokens;
        private ArrayInitParser parser;

        public MainWindowViewModel()
        {
            _parseCommand = new RelayCommand(OnParseCommandExecuted);
        }
    }
}
