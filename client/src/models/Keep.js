import { Profile } from "./Account.js"

export class Keep {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.description = data.description
    this.creator = new Profile(data.creator)
    this.creatorId = data.creatorId
    this.img = data.img
    this.kept = data.kept
    this.name = data.name
    this.views = data.views
    this.vaultKeepId = data.vaultKeepId
  }
}