#!/bin/bash

# Default path for the NuGet packages destination
DEFAULT_DESTINATION="$HOME/projects/local_nuget"

# Function to display help
show_help() {
  echo "Usage: ./build.sh [-o DESTINATION] [-v VERSION] [-help|-h]"
  echo ""
  echo "This script builds a NuGet package and copies the generated file to the specified directory."
  echo "It can also update the version in the package-version.yml file if -v is provided."
  echo ""
  echo "Options:"
  echo "  -o DESTINATION     Directory where the .nupkg package will be copied. If not specified,"
  echo "                     the default directory will be used: $DEFAULT_DESTINATION"
  echo "  -v VERSION         Force the script to update the version in the YAML file to the specified VERSION."
  echo "  -help, -h          Displays this help message."
  echo ""
  echo "Examples:"
  echo "  ./build.sh                # Uses the default directory"
  echo "  ./build.sh -o /my/directory # Uses /my/directory as the destination"
  echo "  ./build.sh -v 2.0.0        # Force version update to 2.0.0"
  exit 0
}

# Check if the user requested help
if [[ "$1" == "-help" || "$1" == "-h" ]]; then
  show_help
fi

# Destination directory (uses parameter or default value)
DESTINATION=$DEFAULT_DESTINATION

# Force version (if provided)
FORCE_VERSION=""

# Parse arguments
while [[ $# -gt 0 ]]; do
  case "$1" in
    -o)
      DESTINATION=$2
      shift 2
      ;;
    -v)
      FORCE_VERSION=$2
      shift 2
      ;;
    *)
      echo "Unknown argument: $1"
      show_help
      ;;
  esac
done

# Path to the YAML file
YAML_FILE="package-version.yml"

# Extract the current version from the YAML file using yq
if ! command -v yq &>/dev/null; then
  echo "Error: yq is not installed. Please install it to continue (https://github.com/mikefarah/yq)."
  exit 1
fi

CURRENT_VERSION=$(yq e '.variables.PACKAGE_VERSION' "$YAML_FILE")

if [ -z "$CURRENT_VERSION" ]; then
  echo "Error: Could not find PACKAGE_VERSION in the YAML file."
  exit 1
fi

echo "Current version in $YAML_FILE: $CURRENT_VERSION"

# If force_version is provided, ask for confirmation
if [ ! -z "$FORCE_VERSION" ]; then
  echo "You are about to change the version from $CURRENT_VERSION to $FORCE_VERSION."
  echo "Please choose an option:"
  echo "1) Use the new version and update the YAML file."
  echo "2) Only generate the package with the new version (do not update YAML)."
  echo "3) Cancel the operation."
  read -p "Enter the number of your choice (1/2/3): " choice

  case $choice in
    1)
      # Use new version and update YAML
      echo "Updating version in $YAML_FILE to $FORCE_VERSION..."
      yq e ".variables.PACKAGE_VERSION = \"$FORCE_VERSION\"" -i "$YAML_FILE"
      echo "Version updated to $FORCE_VERSION."
      ;;
    2)
      # Only generate package without updating YAML
      echo "Generating package with version $FORCE_VERSION, without updating YAML."
      ;;
    3)
      # Cancel the operation
      echo "Operation canceled."
      exit 0
      ;;
    *)
      # Invalid choice
      echo "Invalid choice. Operation canceled."
      exit 1
      ;;
  esac
fi

# Destination directory (ensure it exists)
if [ ! -d "$DESTINATION" ]; then
  echo "Creating destination directory: $DESTINATION"
  mkdir -p "$DESTINATION"
fi

# Build and pack directly to the destination directory
echo "Starting the package build..."
dotnet pack --configuration Release /p:PackageVersion="$FORCE_VERSION" -o "$DESTINATION"

if [ $? -ne 0 ]; then
  echo "Error: Package build failed."
  exit 1
fi

echo "Package built successfully and stored in $DESTINATION."