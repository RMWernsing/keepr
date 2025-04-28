import { Profile } from "./Account.js"

export class Vault {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.description = data.description
    this.img = data.img
    this.isPrivate = data.isPrivate
    this.creator = new Profile(data.creator)
    this.creatorId = data.creatorId
    this.name = data.name
  }
}
