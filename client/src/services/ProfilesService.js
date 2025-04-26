import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Profile } from "@/models/Account.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"

class ProfilesService {
  async getProfileVaults(profileId) {
    AppState.vaults = []
    const response = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log('here are your vaults', response.data)
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