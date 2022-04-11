
  namespace Managers.Template
  {
    public interface IUpdatable 
    {
      void Init();
      void PostInit();
      void Refresh();
      void FixedRefresh();
    }
  }

