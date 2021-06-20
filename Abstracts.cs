namespace PictureProduction
{
    interface IMachine
    {
       IMachine SetNext(IMachine machine);
        // you can add required methods here
        void Handle(Order order, IPicture picture);
    }


    abstract class AbstractMachine : IMachine
    {
        private IMachine _nextMachine;
        public IMachine SetNext(IMachine machine)
        {

            this._nextMachine = machine;

            return machine;
        }
        public virtual void Handle(Order order, IPicture picture)
        {
            if (_nextMachine != null)
                _nextMachine.Handle(order, picture);
        }


    }

}
