using System;
using Core.Actions;
using Core.Utils.ActionUtils.Actions;

namespace spector_core.Actions
{
    public class BaseCommand : BaseAction
    {
        private string _url;
        private string _json;
        private IActionParams _actionParams;
        private Action<string, IActionParams> _fail;
        
        public string Json => _json;

        public string URL => _url;

        public BaseCommand(){}
        
        public BaseCommand(string url, string json, IActionParams actionParams, Action<string> complete, Action<string, IActionParams> fail)
        {
            Init(url, json, actionParams, complete, fail);
        }

        public void Init(string url,string json, IActionParams actionParams, Action<string> complete, Action<string, IActionParams> fail)
        {
            base.Init(complete, null);
            
            _url = url;
            _json = json;
            _actionParams = actionParams;
            _fail = fail;
        }
        
        protected override void Fail(string error = "")
        {
            _fail?.Invoke(error, _actionParams);
        }
    }
}