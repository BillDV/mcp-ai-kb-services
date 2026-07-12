using System;
using System.Collections.Generic;
using System.Text;

namespace mcp_ai_kb_models {
      public sealed class instruction_model : abstract_domain_model {
            public instruction_model(string name, string short_description, string long_description, string knowledge,
                  string knowledege, string[ ] keywords) 
                  : base(name, short_description, long_description, 
                          knowledge, knowledege, keywords) {
            }
      }
}
