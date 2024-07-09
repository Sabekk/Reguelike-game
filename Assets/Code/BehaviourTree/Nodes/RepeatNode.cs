namespace BehaviourTreeSystem
{
    public class RepeatNode : DecoratorNode
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void OnStart()
        {
          
        }

        protected override void OnStop()
        {
           
        }

        protected override NodeState OnUpdate()
        {
            Child.Update();
            return NodeState.RUNNING;
        }

        #endregion
    }
}