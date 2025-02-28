﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace VkNet.Enums.SafetyEnums;

/// <summary>
/// Тип шаблона.
/// </summary>
[StringEnum]
[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum TemplateType
{
	/// <summary>
	/// Карусель
	/// </summary>
	Carousel
}