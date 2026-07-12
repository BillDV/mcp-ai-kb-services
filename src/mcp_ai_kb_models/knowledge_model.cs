using System;
using System.Collections.Generic;
using System.Text;

namespace mcp_ai_kb_models {
      public sealed class knowledge_model {
            public int            id                    { get; private set; }
            public string         name                  { get; private set; }
            public knowledge_type knowledge             { get; private set; }
            public string         short_description     { get; private set; }
            public DateTime       created_utc           { get; private set; }
            public DateTime       updated_utc           { get; private set; }
            public Int16          word_count            { get; private set; }
            public Int16          line_count            { get; private set; }
            public Int16          estimated_token_count { get; private set; }

      } 
  }

