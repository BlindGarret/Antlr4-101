/****************************************************************************/
// PROJECT      : Mnts.Controls - An MVVM Command 
//---------------------------------------------------------------------------
//
// INFORMATION  : 
// File         : RelayCommand.cs 
// Details      : Use if PRISM isn't referenced.
// Copyright    : Copyright © Moore Nanotechnology Systems, LLC 2013
// Contributor  : Ed Duer
// Created      : 3/29/2013 11:55 AM
/****************************************************************************/

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace StarterProject
{
    /// <summary>
    /// Provides a simple class to implement ICommand objects in view models.
    /// Use if PRISM isn't referenced.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Properties
        /// <summary>
        /// Bind to enable input gestures executing this command.
        /// </summary>
        public InputGestureCollection InputGestures { get; private set; }

        /// <summary>
        /// Bind in an optional parameter.
        /// </summary>
        public object ConstantParameter { get; set; }
        #endregion

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged = delegate { };

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="execute">The delegate to call when this command is executed.</param>
        /// <exception cref="ArgumentNullException">execute cannot be null.</exception>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="execute">The delegate to call when this command is executed.</param>
        /// <param name="canExecute">The delegate which determines whether or not this 
        /// command can execute.</param>
        /// <exception cref="ArgumentNullException">execute cannot be null.</exception>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="execute">The delegate to call when this command is executed.</param>
        /// <param name="canExecute">The delegate which determines whether or not this 
        /// command can execute.</param>
        /// <param name="inputGestures">The gestures to bind to this command.</param>
        /// <exception cref="ArgumentNullException">execute cannot be null.</exception>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute, InputGestureCollection inputGestures)
            : this(execute, canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (inputGestures != null)
            {
                InputGestures = new InputGestureCollection();
                InputGestures.AddRange(inputGestures);
            }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter ?? ConstantParameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            _execute(parameter ?? ConstantParameter);
        }

        /// <summary>
        /// Call to update the enabled state of this command.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate",
            Justification = "This is used for ICommand, not events.")]
        public void RaiseCanExecuteChanged()
        {
            var canExecuteChanged = CanExecuteChanged;
            if (canExecuteChanged != null)
            {
                canExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
