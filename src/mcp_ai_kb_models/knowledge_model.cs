using System;
using System.Collections.Generic;
using System.Text;

namespace mcp_ai_kb_models {
      public sealed class knowledge_model : abstract_domain_model {

            public knowledge_model(string name, string short_description, string long_description,
                  string knowledge, string[] keywords)
                  : base(name, short_description, long_description, 
                          knowledge, keywords) {
            }

      } 
  }


