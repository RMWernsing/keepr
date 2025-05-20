# Use the official Node.js image as the base image
FROM node:23-alpine

# Set the working directory inside the container
WORKDIR /app

# Copy the package.json and package-lock.json files to the working directory
COPY package*.json ./

# Install the application dependencies
RUN npm install --production

# Copy the application code to the working directory
COPY . .

# Start the application
CMD ["npm", "start"]
