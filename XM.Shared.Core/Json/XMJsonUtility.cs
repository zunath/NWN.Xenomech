using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using XM.Shared.Core.Json.Converter;
using XM.Shared.Core.Json.Converter.UI.Factory;
using XM.Shared.Core.Json.Converter.UI.Layout;
using XM.Shared.Core.Json.Converter.UI;
using XM.Shared.Core.Json.Converter.UI.Binding;
using XM.Shared.Core.Json.Converter.UI.Layout;
using XM.Shared.Core.Json.Converter.UI.Widget;
using XM.Shared.Core.Json.Converter.UI.Widget.DrawList;
using NuiValueStrRefConverter = XM.Shared.Core.Json.Converter.UI.Binding.NuiValueStrRefConverter;

namespace XM.Shared.Core.Json
{
    public static class XMJsonUtility
    {
        private static readonly JsonSerializerOptions _options;

        static XMJsonUtility()
        {
            _options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            };

            RegisterConverters();
        }

        private static void RegisterConverters()
        {
            RegisterBindingConverters();
            RegisterLayoutConverters();
            RegisterWidgetConverters();
            RegisterDrawListConverters();
            RegisterElementConverters();
            RegisterOtherConverters();
        }

        private static void RegisterBindingConverters()
        {
            _options.Converters.AddRange([
                new NuiBindConverterFactory(),
                new NuiBindStrRefConverter(),
                new NuiPropertyConverterFactory(),
                new NuiValueConverterFactory(),
                new NuiValueStrRefConverter()]);
        }

        private static void RegisterLayoutConverters()
        {
            _options.Converters.AddRange([
                new NuiColumnConverter(),
                new NuiGroupConverter(),
                new NuiLayoutConverter(),
                new NuiRowConverter()]);
        }

        private static void RegisterDrawListConverters()
        {
            _options.Converters.AddRange([
                new NuiDrawListArcConverter(),
                new NuiDrawListCircleConverter(),
                new NuiDrawListCurveConverter(),
                new NuiDrawListImageConverter(),
                new NuiDrawListItemConverter(),
                new NuiDrawListLineConverter(),
                new NuiDrawListPolyLineConverter(),
                new NuiDrawListTextConverter()]);
        }

        private static void RegisterWidgetConverters()
        {
            _options.Converters.AddRange([
                new NuiButtonConverter(),
                new NuiButtonImageConverter(),
                new NuiButtonSelectConverter(),
                new NuiChartConverter(),
                new NuiChartSlotConverter(),
                new NuiCheckConverter(),
                new NuiColorPickerConverter(),
                new NuiComboConverter(),
                new NuiComboEntryConverter(),
                new NuiImageConverter(),
                new NuiLabelConverter(),
                new NuiOptionsConverter(),
                new NuiProgressConverter(),
                new NuiSliderConverter(),
                new NuiSliderFloatConverter(),
                new NuiSpacerConverter(),
                new NuiTextConverter(),
                new NuiTextConverter(),
                new NuiTogglesConverter()]);
        }

        private static void RegisterElementConverters()
        {
            _options.Converters.AddRange([
                new NuiElementConverter(),
                new NuiRectConverter(),
                new NuiVectorConverter(),
                new NuiWindowConverter()]);
        }

        private static void RegisterOtherConverters()
        {
            _options.Converters.AddRange([
                new ColorConverter()]);
        }

        /// <summary>
        /// Deserializes a JSON string.
        /// </summary>
        /// <param name="json">The JSON to deserialize.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _options);
        }

        /// <summary>
        /// Serializes a value as JSON.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <returns>A JSON string representing the value.</returns>
        public static string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, _options); 
        }

        public static object DeserializeObject(string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, _options);
        }

    }
}
