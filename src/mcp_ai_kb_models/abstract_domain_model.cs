using System;
using System.Collections.Generic;
using System.Text;

namespace mcp_ai_kb_models {
      public abstract class abstract_domain_model {
            public string name {
                  get; private set;
            }
            public string short_description {
                  get; private set;
            }
            public string long_description {
                  get; private set;
            }
            public string knowledge {
                  get; private set;
            }
            public string[] keywords {
                  get; private set;
            }

            protected abstract_domain_model(string name, string short_description, 
                  string long_description, string knowledge, string knowledege, 
                  string[] keywords) {
                  this.name = name;
                  this.short_description = short_description;
                  this.long_description = long_description;
                  this.knowledge = knowledege;
                  this.keywords = keywords;
            }

            protected abstract_domain_model(string name, string short_description, string long_description, string knowledge, string[ ] keywords) {
                  this.name = name;
                  this.short_description = short_description;
                  this.long_description = long_description;
                  this.knowledge = knowledge;
                  this.keywords = keywords;
            }
      }
}
