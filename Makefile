FRONT_END_BINARY=frontApp
BROKER_BINARY=brokerApp
AUTH_BINARY=authApp

## up: starts all containers in the background without forcing build
up:
	@echo "Starting Docker images..."
	docker-compose up
	@echo "Docker images started!"

## up_build: stops docker-compose (if running), builds all projects and starts docker compose
up_build: build_motorcycle build_user build_user build_rent build_plan
	@echo "Stopping docker images (if running...)"
	docker-compose down
	@echo "Building (when required) and starting docker images..."
	docker-compose up --build
	@echo "Docker images built and started!"

## down: stop docker compose
down:
	@echo "Stopping docker compose..."
	docker-compose down
	@echo "Done!"

## build_motorcycle: builds the motorcycle api binary as a linux executable
build_motorcycle:
	@echo "Building motorcycle binary..."
	docker build -f src/services/motorcycle/Motorcycle.API/Dockerfile  .
	@echo "Done!"

## build_user: builds the user api binary as a linux executable
build_user:
	@echo "Building user binary..."
	docker build -f src/services/user/User.API/Dockerfile  .
	@echo "Done!"

## build_rent: builds the rent api binary as linux executable
build_rent:
	@echo "Building rent end binary..."
	docker build -f src/services/rent/Rent.API/Dockerfile  .
	@echo "Done!"

## build_plan: builds the plan api binary as linux executable
build_plan:
	@echo "Building plan api end binary..."
	docker build -f src/services/plan/Plan.API/Dockerfile  .
	@echo "Done!"

clean:
	@echo "Cleaning all..."
	docker system prune
	@echo "Done!"

test:
	@echo "Testing all..."
	dotnet clean
	dotnet test
	@echo "Done!"

