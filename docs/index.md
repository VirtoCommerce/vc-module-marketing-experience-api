# Overview

[![CI status](https://github.com/VirtoCommerce/vc-module-marketing-experience-api/workflows/Module%20CI/badge.svg?branch=dev)](https://github.com/VirtoCommerce/vc-module-marketing-experience-api/actions?query=workflow%3A"Module+CI") [![Quality gate](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-marketing-experience-api&metric=alert_status&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-marketing-experience-api) [![Reliability rating](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-marketing-experience-api&metric=reliability_rating&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-marketing-experience-api) [![Security rating](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-marketing-experience-api&metric=security_rating&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-marketing-experience-api) [![Sqale rating](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-marketing-experience-api&metric=sqale_rating&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-marketing-experience-api)

X-Marketing provides high performance search queries for dynamic content.

## How to Use

Please refer to [Experience API Overview](https://docs.virtocommerce.org/modules/experience-api/) to for more information on how to use X-Marketing.

## QueryRoot
### Evaluate dynamic content

This query allows you to evaluate dynamic content based on:
  - dynamic content place
  - store
  - product
  - category
  - tags
  - user groups

#### Definition

```
evaluateDynamicContent(
  storeId: String, 
  placeName: String, 
  categoryId: String, 
  productId: String, 
  cultureName: String, 
  toDate: DateTime, 
  tags: [String], 
  userGroups: [String])
)
```

#### Arguments
|#|Name        |Type                       |Description                |
|-|------------|---------------            |---------------------------|
|1|storeId     |StringGraphType            |Store Id                   |
|2|placeName   |StringGraphType            |Dynamic content place name |
|3|categoryId  |StringGraphType            |Category Id                |
|4|productId   |StringGraphType            |Product Id                 |
|5|cultureName |StringGraphType            |Culture name (e.g. "en-US")|
|6|toDate      |StringGraphType            |Evaluation date            |
|7|tags        |List of StringGraphType    |List of tags               |
|8|userGroups  |List of StringGraphType    |List of user groups|

#### Example

```json
{
  evaluateDynamicContent(
    storeId: "B2B-store"
    placeName: "MainSlider"
    tags: ["Main"]
    userGroups: ["Customers"]
    productId: "8b7b07c165924a879392f4f51a6f7ce0"
  ) {
    items {
      id
      name
      contentType
      dynamicProperties {
        name
        value
      }
    }
    totalCount
  }
}
```
