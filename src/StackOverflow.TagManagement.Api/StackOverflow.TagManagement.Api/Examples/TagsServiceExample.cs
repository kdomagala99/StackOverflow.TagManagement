using System.Text.Json;
using Swashbuckle.AspNetCore.Filters;

namespace StackOverflow.TagManagement.Api.Examples
{
    public class TagsServiceListExample : IExamplesProvider<JsonDocument>
    {
        public JsonDocument GetExamples() => JsonDocument.Parse(@"{
  ""data"": [
    {
      ""tagPercentage"": 0.0009653255823841126,
      ""collectives"": null,
      ""count"": 999,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""pyparsing"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009653255823841126,
      ""collectives"": null,
      ""count"": 999,
      ""has_synonyms"": true,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""telerik-mvc"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009653255823841126,
      ""collectives"": null,
      ""count"": 999,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""joi"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009653255823841126,
      ""collectives"": null,
      ""count"": 999,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""mobile-development"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009653255823841126,
      ""collectives"": null,
      ""count"": 999,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""macos-big-sur"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009643592905098543,
      ""collectives"": null,
      ""count"": 998,
      ""has_synonyms"": true,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""truetype"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009643592905098543,
      ""collectives"": null,
      ""count"": 998,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""url-encoding"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009643592905098543,
      ""collectives"": null,
      ""count"": 998,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""subscribe"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009643592905098543,
      ""collectives"": null,
      ""count"": 998,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""scene"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009633929986355959,
      ""collectives"": null,
      ""count"": 997,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""bluez"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009633929986355959,
      ""collectives"": null,
      ""count"": 997,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""playframework-2.1"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009633929986355959,
      ""collectives"": null,
      ""count"": 997,
      ""has_synonyms"": true,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""tinkerpop"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009633929986355959,
      ""collectives"": null,
      ""count"": 997,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""polynomials"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009633929986355959,
      ""collectives"": null,
      ""count"": 997,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""konvajs"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009624267067613376,
      ""collectives"": null,
      ""count"": 996,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""oncreate"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009614604148870791,
      ""collectives"": null,
      ""count"": 995,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""interaction"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009604941230128207,
      ""collectives"": null,
      ""count"": 994,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""onactivityresult"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009604941230128207,
      ""collectives"": null,
      ""count"": 994,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""shiny-reactivity"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009604941230128207,
      ""collectives"": null,
      ""count"": 994,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""promql"",
      ""synonyms"": null,
      ""user_id"": null
    },
    {
      ""tagPercentage"": 0.0009604941230128207,
      ""collectives"": null,
      ""count"": 994,
      ""has_synonyms"": false,
      ""is_moderator_only"": false,
      ""is_required"": false,
      ""last_activity_date"": null,
      ""name"": ""flutter-bloc"",
      ""synonyms"": null,
      ""user_id"": null
    }
  ],
  ""error"": null
}");
    }
}
