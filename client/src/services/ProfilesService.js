import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Account, Profile } from "@/models/Account.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"
import { Keep } from "@/models/Keep.js"

class ProfilesService {
  async editProfile(accountData) {
    const response = await api.put('account', accountData)
    logger.log('here is your new account data', response.data)
    const account = new Account(response.data)
    AppState.account = account
  }
  async getProfileKeeps(profileId) {
    AppState.keeps = []
    const response = await api.get(`api/profiles/${profileId}/keeps`)
    // logger.log('here are your keeps for your profile', response.data)
    const keeps = response.data.map(pojo => new Keep(pojo))
    AppState.keeps = keeps
  }
  async getProfileVaults(profileId) {
    AppState.vaults = []
    const response = await api.get(`api/profiles/${profileId}/vaults`)
    // logger.log('here are your vaults', response.data)
    const vaults = response.data.map(pojo => new Vault(pojo))
    AppState.vaults = vaults
  }
  async getProfile(profileId) {
    AppState.activeProfile = null
    const response = await api.get(`api/profiles/${profileId}`)
    // logger.log('here is your profile', response.data)
    const profile = new Profile(response.data)
    AppState.activeProfile = profile
  }

}

export const profilesService = new ProfilesService()