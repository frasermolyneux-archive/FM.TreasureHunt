resource "azurerm_app_service_plan" "app-service-plan" {
    name = "FM-TreasureHunt-Web-AppPlan-${var.environment}"
    resource_group_name = azurerm_resource_group.resource-group.name
    location = azurerm_resource_group.resource-group.location

    sku {
        tier = "Shared"
        size = "D1"
    }
}

resource "azurerm_app_service" "app-service" {
  name = "FM-TreasureHunt-WebApp-${var.environment}"
  location = var.region
  resource_group_name = azurerm_resource_group.resource-group.name
  app_service_plan_id = azurerm_app_service_plan.app-service-plan.id
}