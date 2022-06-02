﻿using System;
using FluentAssertions;
using NUnit.Framework;
using VkNet.Enums;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams.Ads;
using VkNet.Tests.Infrastructure;

namespace VkNet.Tests.Categories.Ads
{
	[TestFixture]

	public class CreateAdsTest : CategoryBaseTest
	{
		protected override string Folder => "Ads";

		[Test]
		public void AddOfficeUsers()
		{
			Url = "https://api.vk.com/method/ads.createAds";

			ReadCategoryJsonPath(nameof(Api.Ads.CreateAds));

			var adSpecification1 = new AdSpecification
			{
				CampaignId = 1012219949,
				GoalType = GoalType.Cpm,
				AgeRestriction = AdAgeRestriction.NoRestriction,
				Name = "123",
				CostType = CostType.Cpc,
				AdFormat = AdFormat.AdaptiveFormat,
				Cpc = 3156,
				AdPlatform = AdPlatform.All,
				LinkUrl = new Uri("https://vk.com/nixus9?w=wall-126102803_64")
			};

			var adSpecification2 = new AdSpecification
			{
				CampaignId = 1012219949,
				GoalType = GoalType.Cpm,
				AgeRestriction = AdAgeRestriction.NoRestriction,
				Name = "123",
				CostType = CostType.Cpc,
				AdFormat = AdFormat.AdaptiveFormat,
				Cpc = 3156,
				AdPlatform = AdPlatform.All,
				LinkUrl = new Uri("https://vk.com/nixus9?w=wall-126102803_64")
			};

			AdSpecification[] data =
			{
				adSpecification1,
				adSpecification2
			};

			var officeUsers = Api.Ads.CreateAds(new AdsDataSpecificationParams<AdSpecification>
			{
				Data = data,
				AccountId = 1605245430
			});

			officeUsers[0].Id.Should().Be(1);
			officeUsers[0].ErrorCode.Should().Be(100);
			officeUsers[1].Id.Should().Be(2);
		}
	}
}